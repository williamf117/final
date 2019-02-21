using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum missionType
    {
        Escort,
        kill,
        fetch
    }
public abstract class MissionBase : MonoBehaviour
{
    

    missionType type;
    List<Fleet> encounters = new List<Fleet>();
    string missiondiscription = "go here do this";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Print()
    {
        InGameMenue drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
      
    }
}
