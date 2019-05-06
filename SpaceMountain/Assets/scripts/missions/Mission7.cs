using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mission7 : MissionBase
{
    bool fin = false;
    // Start is called before the first frame update
    void Start()
    {
        description = "In order to finish off the pirates we need a staging station in the orbit of Neptune. But be careful the pirates are going to through everything they have at you. You will have to defend this station while it is coming online.";
        EventManager.AddMissionCompleatInvokers(this);

    }

    // Update is called once per frame
    void Update()
    {
        drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        if (SceneManager.GetActiveScene().name == "neptune" &&fin==false)
        {
            Fleet final = Instantiate((GameObject)Resources.Load("prefabs/Prefabfleet")).GetComponent<Fleet>();
            final.MakeFleet(15);
            final.AddShip((GameObject)Resources.Load("prefabs/Carrier 1"));
            final.AddShip((GameObject)Resources.Load("prefabs/Carrier 1"));
            final.AddShip((GameObject)Resources.Load("prefabs/Carrier 1"));
            final.AddShip((GameObject)Resources.Load("prefabs/Carrier 1"));
            final.Target = GameObject.Find("player");
            GameManager.instance.BattleSceneName = "BaseBattle";
            fin = true;
        }
        else if (SceneManager.GetActiveScene().name == "neptune" && fin)
        {
            missioncomplete.Invoke("Thats the last of them ");
            completed = true;


        }
    }
}
