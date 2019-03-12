using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    // the method to get access to the game manager methods and properties - ADDING STUFF
    public static EventManager Instance { get; private set; }

    //// making the constructor private so no other source and create it
    private EventManager() { }

    // will set the instance and then initialize the room
    private void Awake()
    {
        // if nothing in assigned to the instance property
        if (Instance == null)
        {
            Instance = new EventManager();
            DontDestroyOnLoad(gameObject);
        }
        else // if there is already an instance, it will destroy itself
        {
            Destroy(gameObject);
        }
    }

    static List<Satellite> enterplanetInvoker=new List<Satellite>();
    static List<UnityAction<string>> enterplanetListeners=new List<UnityAction<string>>();

    static List<Fleet> enterbattleInvoker = new List<Fleet>();
    static List<UnityAction<List<GameObject>>> enterbattlelisteners = new List<UnityAction<List<GameObject>>>();

    static List<BattleControler> endBattleinvoker = new List<BattleControler>();
    static List<UnityAction> endbattlelisteners = new List<UnityAction>();

    static List<MissionBase> MissionCompleateInvokers = new List<MissionBase>();
    static List<UnityAction<string>> MissionCompleateListeners = new List<UnityAction<string>>();

    public static void AddMissionCompleatInvokers(MissionBase invoker)
    {
        MissionCompleateInvokers.Add(invoker);
        foreach(UnityAction<string> listener in MissionCompleateListeners)
        {
            invoker.AddMissionCompleteListener(listener);
        }
    }

    public static void AddMissionCompleteListeners(UnityAction<string> listener)
    {
        MissionCompleateListeners.Add(listener);
        foreach(MissionBase invoker in MissionCompleateInvokers)
        {
            invoker.AddMissionCompleteListener(listener);
        }
    }

    #region entyerplanet event
    public static void AddNewEnterPlanetInvoker(Satellite invoker)
    {
        // add invoker to list and add all listeners to invoker
        enterplanetInvoker.Add(invoker);
        foreach (UnityAction<string> listener in enterplanetListeners)
        {
            invoker.AddEnterPlanetListener(listener);
        }
    }

    /// <summary>
    /// Adds enemy death listener
    /// </summary>
    /// <param name="listener">invoker</param>
    public static void AddNewenterPlanetListener(UnityAction<string> listener)
    {
        // add listener to list and to invokers
        enterplanetListeners.Add(listener);
        foreach (Satellite invoker in enterplanetInvoker)
        {
            invoker.AddEnterPlanetListener(listener);
        }
    }
    #endregion
    #region enter batttle 

    public static void AddEnterBattleInvoker(Fleet invoker)
    {
        // add invoker to list and add all listeners to invoker
        enterbattleInvoker.Add(invoker);
        foreach (UnityAction<List<GameObject>> listener in enterbattlelisteners)
        {
            invoker.AddEnterBattleListener(listener);
        }
    }

    /// <summary>
    /// Adds enemy death listener
    /// </summary>
    /// <param name="listener">invoker</param>
    public static void AddNewEnterBattleListener(UnityAction<List<GameObject>> listener)
    {
        // add listener to list and to invokers
        enterbattlelisteners.Add(listener);
        foreach (Fleet invoker in enterbattleInvoker)
        {
            invoker.AddEnterBattleListener(listener);
        }
    }
    #endregion

    public static void AddleveBattleinvoker(BattleControler invoker)
    {
        // add invoker to list and add all listeners to invoker
        endBattleinvoker.Add(invoker);
        foreach (UnityAction listener in endbattlelisteners)
        {
            invoker.addEndBattleListener(listener);
        }
    }

    /// <summary>
    /// Adds enemy death listener
    /// </summary>
    /// <param name="listener">invoker</param>
    public static void AddEndbattlelistener(UnityAction listener)
    {
        // add listener to list and to invokers
        endbattlelisteners.Add(listener);
        foreach (BattleControler invoker in endBattleinvoker)
        {
            invoker.addEndBattleListener(listener);

        }
    }
}
