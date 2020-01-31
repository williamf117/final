using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mission6 : MissionBase
{
    bool fleetSpawned = false;
    Fleet boss;
    // Start is called before the first frame update
    void Start()
    {
        description = "In an effort to make up their losses a group of capital ships has attacked the martian dockyard at Deimos keep the Pirates from rebuilding their lost ships.";
        reward = 200000;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            EventManager.AddMissionCompleatInvokers(this);
            drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
            //if we are in the mars scene load the eanamy fleet 
            if (SceneManager.GetActiveScene().name == "Mars")
            {

                desto = GameObject.Find("Deimos");
                //spawn the fleet if it is not already spawned 
                if (!fleetSpawned)
                {
                    fleetSpawned = true;
                    GameObject f = Instantiate((GameObject)(Resources.Load("prefabs/Prefabfleet")));
                    f.transform.position = desto.transform.position;
                    fleetSpawned = true;
                    boss = f.GetComponent<Fleet>();
                    f.GetComponent<Fleet>().Target = desto;
                    f.GetComponent<Fleet>().MakeFleet(8);
                    f.GetComponent<Fleet>().AddShip((GameObject)Resources.Load("prefabs/Carrier 1"));
                    f.GetComponent<Fleet>().AddShip((GameObject)Resources.Load("prefabs/Carrier 1"));
                }
                if (fleetSpawned && boss == null && SceneManager.GetActiveScene().name == "Mars" && !completed)
                {
                    completed = true;
                    missioncomplete.Invoke("Mission 6 complete ");
                }

            }
        }
        catch
        {
            Debug.Log("mission 6 bug");
        }
    }
}
