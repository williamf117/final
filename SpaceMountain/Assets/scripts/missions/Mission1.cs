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
    // Start is called before the first frame update
    void Start()
    {
        reward = 10000;
        description = "go to IO and destroy the fleet there";
        //DontDestroyOnLoad(this);
        desto = GameObject.Find("Io");
        prefabFleet = (GameObject) Resources.Load("prefabs/Prefabfleet");
         fleet = Instantiate(prefabFleet);
        fleet.GetComponent<Fleet>().Target = desto;
        fleet.transform.position = GameObject.Find("Callisto").transform.position;
        fleet.GetComponent<Fleet>().MakeFleet(3);
        drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        EventManager.AddMissionCompleatInvokers(this);
       
        EventManager.AddEndbattlelistener(compleatMission);
    }

    // Update is called once per frame
    void Update()
    {
        if (fleet==null && SceneManager.GetActiveScene().name == "Jupiter")
        {
            completed = true;
            missioncomplete.Invoke("Mission 1 Complete");
            
        }
    }
    public override void Print()
    {
        drawmission.MissionBreafing(description + " reward:" + reward);
    }

}
