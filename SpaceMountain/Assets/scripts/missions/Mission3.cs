using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3 : MissionBase
{
    float StartFunds;
    // Start is called before the first frame update
    void Start()
    {
        StartFunds = GameManager.instance.Funds;
        description = "open the store and buy your first new ship";
        reward = 500;
        EventManager.AddMissionCompleatInvokers(this);
    }

    // Update is called once per frame
    void Update()
    {
        drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        if (GameManager.instance.Funds < StartFunds)
        {
            missioncomplete.Invoke("mission3 complete");
            completed = true;
            StartFunds = GameManager.instance.Funds;
        }
    }
}
