using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum missionType
{
    Escort,
    kill,
    fetch
}

public class Mission1 : MissionBase
{
    [SerializeField]
    GameObject fleet;
    bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        reward = 500;
        description = "go to IO and destroy the fleet there";
        //DontDestroyOnLoad(this);
        desto = GameObject.Find("Io");
        prefabFleet = (GameObject) Resources.Load("prefabs/Prefabfleet");
        
        drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        EventManager.AddMissionCompleatInvokers(this);
       
        EventManager.AddEndbattlelistener(compleatMission);
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if we are in the right scene and make a fleet 
        if (SceneManager.GetActiveScene().name == "Jupiter" && init==false)
        {
            init = true;
            fleet = Instantiate(prefabFleet);
            fleet.GetComponent<Fleet>().Target = desto;
            fleet.transform.position = GameObject.Find("Callisto").transform.position;
            fleet.GetComponent<Fleet>().MakeFleet(3);
        }
       if (SceneManager.GetActiveScene().name!="testBattle")
             drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        if (fleet==null && SceneManager.GetActiveScene().name == "Jupiter"&&init==true)
        {
            completed = true;
            missioncomplete.Invoke("Mission 1 complete");
            
        }
    }
    public override void Print()
    {
        drawmission.MissionBreafing(description + " reward:" + reward);
    }

}
