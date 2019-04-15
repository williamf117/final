using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages audio across entire game
/// </summary>
public class AudioManager
{
    #region Fields

    static bool initialized = false;                            // bool set up
    static AudioSource audioSource;                             // store audio source
    static Dictionary<AudioClipName, AudioClip> audioClips =    // set up dictionary
        new Dictionary<AudioClipName, AudioClip>();
    private static AudioManager instance;                       // Singleton setup

    #endregion

    #region Properties

    /// <summary>
    /// Gets whether audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Construct private singleton
    /// </summary>
    /// <param name="AudioManager"></param>
    private AudioManager() { }

    #endregion

    #region Methods

    /// <summary>
    /// Singleton Instance method
    /// </summary>
    /// <param name="Instance"></param>
    public static AudioManager Instance
    {
        // creates new audio manager if none exists
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance;

        }
    }

    /// <summary>
    /// Initializes audio manager
    /// </summary>
    /// <param name="source"></param>
    public static void Initialize(AudioSource source)
    {
        // loads in sounds from Resources
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.SpaceLoop, (AudioClip)Resources.Load("sounds/Space Station SLOW LOOP"));

        audioClips.Add(AudioClipName.BattleLoop, (AudioClip)Resources.Load("sounds/Long Range Combat LOOP"));
        Debug.Log(audioClips[AudioClipName.BattleLoop]);



    }

    /// <summary>
    /// Plays audio clip with given name
    /// </summary>
    /// <param name="name">name of sound to play</param>
    public void Play(AudioClipName name)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClips[name]);
        }
    }
   

    /// <summary>
    /// Overlaps audio clip with given name
    /// </summary>
    /// <param name="name">name of sound to play</param>
    public void Overlap(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    /// <summary>
    /// Stops audio clip with given name
    /// </summary>
    /// <param name="name">name of sound to play</param>
    public void StopSource(AudioClipName name)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    #endregion
}
