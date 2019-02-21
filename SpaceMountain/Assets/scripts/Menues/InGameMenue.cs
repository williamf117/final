using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenue : MonoBehaviour
{
    [SerializeField]
    HUD navMenue;
    [SerializeField]
    Text funds;
    Player playerscript;
    [SerializeField]
    GameObject missionBreefing;
    [SerializeField]
    Canvas hud;
    // Start is called before the first frame update
    void Start()
    {
        playerscript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void navigation()
    {
        navMenue.CreateMenue();
    }
    public void Mission()
    {
        Debug.Log("mission");
        MissionBreafing("this is a description ");

    }
    public void Fleet()
    {
        Debug.Log("fleet");
    }
    public void ToSole()
    {
        Camera.main.GetComponent<GameManager>().changesene("sole");
    }
    public void MissionBreafing(string descriptionString)
    {
        GameObject mb = Instantiate(missionBreefing);
        mb.transform.SetParent(hud.transform, false);
        Text description = mb.transform.Find("description").GetComponent<Text>();
        description.text = "this is a mission description";
    }

}
