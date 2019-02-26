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
public class MissionBase : MonoBehaviour
{
    InGameMenue drawmission;
    missionType type=missionType.kill;
    List<GameObject> encounters = new List<GameObject>();
    string missiondiscription = "go here do this";
    GameObject desto;
    string description = "travle to the moon of IO and destroy the fleet there";
    public int reward = 1000;
    bool acsepted=true;
    [SerializeField]
    GameObject PrefabFleet;
    public bool completed = false;
   
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        desto = GameObject.Find("Io");
        GameObject fleet= Instantiate(PrefabFleet);
        fleet.GetComponent<Fleet>().Target = desto;
        encounters.Add(PrefabFleet);
        drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        drawmission.CurrentMIssion = this;
        fleet.transform.position = GameObject.Find("Callisto").transform.position;
        EventManager.AddEndbattlelistener(compleatMission);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Print()
    {
        
        drawmission.MissionBreafing(description +" reward:"+reward );
    }
    public void compleatMission()
    {
        completed = true;
    }
}
