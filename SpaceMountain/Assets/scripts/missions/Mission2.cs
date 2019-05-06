using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission2 : MissionBase
{
    bool fleetExists = false;

    private void Start()
    {
        drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        reward = 3000;
        description = "There has been a brake out of the logic plague on the moon of Phobos in orbit of mars. You must travel to mars with medical equipment";
        prefabFleet = (GameObject)Resources.Load("prefabs/Prefabfleet");
        EventManager.AddMissionCompleatInvokers(this);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "testBattle"|| SceneManager.GetActiveScene().name != "Menue")
        {
            drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        }
        if (SceneManager.GetActiveScene().name != "sole" && SceneManager.GetActiveScene().name != "Mars")
        {
            desto = null;
        }
      
        else if (SceneManager.GetActiveScene().name == "sole" &&!fleetExists)
        {
            desto = GameObject.Find("Mars");
            GameObject fleet= Instantiate(prefabFleet);
            fleet.transform.position = desto.transform.position;

            fleet.GetComponent<Fleet>().MakeFleet(5);
            fleet.GetComponent<Fleet>().Target = GameObject.FindGameObjectWithTag("Player");
            fleetExists = true;
           
            

        }
        else if (SceneManager.GetActiveScene().name == "Mars")
        {
            desto = GameObject.Find("Phobos");

            if ((GameObject.Find("player").transform.position - desto.transform.position).magnitude < 3)
            {
                completed = true;
                missioncomplete.Invoke("Mission2 complete");
            }
        }

    }


}

