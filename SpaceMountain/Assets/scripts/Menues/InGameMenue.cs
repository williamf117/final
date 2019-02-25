using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenue : MonoBehaviour
{
    [SerializeField]
    HUD navMenue;
    [SerializeField]
    Text funds,Fule;
    Player playerscript;
    [SerializeField]
    GameObject missionBreefing;
    [SerializeField]
    Canvas hud;
    GameObject mb;
    MissionBase currMission;
    public MissionBase CurrentMIssion
    {
        get { return currMission; }
        set { currMission = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerscript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        funds.text = "Funds: "+playerscript.Funds.ToString();
        Fule.text = "Fule: " + playerscript.Fule.ToString();
    }

    public void navigation()
    {
        navMenue.CreateMenue();
    }
     public void Mission()
    {
        Debug.Log("mission");
        //MissionBreafing("this is a description ");
        currMission.Print();

    }
    public void Fleet()
    {
        Debug.Log("fleet");
    }
    public void ToSole()
    {
        
    }
    public void MissionBreafing(string descriptionString)
    {
         mb = Instantiate(missionBreefing);
        mb.transform.SetParent(hud.transform, false);
        Text description = mb.transform.Find("description").GetComponent<Text>();
        description.text = descriptionString;
    }


}
