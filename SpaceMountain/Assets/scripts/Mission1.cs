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
public class Mission1 : MissionBase
{







    


    // Start is called before the first frame update
    void Start()
    {
        reward = 10000;
        description = "go to IO and destroy the fleet there";
        //DontDestroyOnLoad(this);
        desto = GameObject.Find("Io");
        GameObject fleet = Instantiate(PrefabFleet);
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

}
