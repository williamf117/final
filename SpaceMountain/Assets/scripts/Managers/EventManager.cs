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
}
