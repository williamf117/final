using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission4 : MissionBase
{
    GameObject Titan;
    GameObject fleet;
    bool fleetLoaded = false;
    bool battlefinished = false;
    // Start is called before the first frame update
    void Start()
    {
        reward = 3000;
        description = "We have received a destress signal from a stranded titan class vessel pirates are closing in. Get there before they do and you might get some extra ships from the earth fleet.";
        EventManager.AddEndbattlelistener(EndBattle);
    }

    // Update is called once per frame
    void Update()
    {
        if (battlefinished==true && SceneManager.GetActiveScene().name == "earth")
        {

            completed = true;
            missioncomplete.Invoke("Mission 4 Complete");
            GameManager.instance.playerfleet.Remove(Titan);
        }
        else if (SceneManager.GetActiveScene().name == "earth"&& !fleetLoaded && !battlefinished)
        {
            fleetLoaded = true;
            desto = GameObject.Find("Luna");
             fleet= Instantiate(prefabFleet);
            fleet.GetComponent<Fleet>().MakeFleet(9);
            fleet.GetComponent<Fleet>().Target = GameObject.Find("Luna");

            Titan = (GameObject)Resources.Load("prefabs/Titan");
            GameManager.instance.PlayerFleet.Add(Titan);//add a titan to the players fleet 
           
            

        }
        if (SceneManager.GetActiveScene().name != "testBattle")
        {
            drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();//keep the draw mission variable populated accross scenes 
        }
        //keep the desto accureat
        if (SceneManager.GetActiveScene().name == "sole")
        {
            desto = GameObject.Find("earth");
        }
        
        
       

    }
    void EndBattle()
    {
        battlefinished = true;
    }
}
