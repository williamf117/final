using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    GameManager gm;
  // before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        EventManager.AddMissionCompleteListeners(MissionBreafing);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        playerscript = GameObject.FindWithTag("Player").GetComponent<Player>();
        funds.text = "Funds: "+playerscript.Funds.ToString();
        Fule.text = "Fuel: " + playerscript.Fule.ToString();
        currMission = gm.CurrMission;
    }

    public void navigation()
    {
        AudioManager.Instance.Play(AudioClipName.Button);
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
        AudioManager.Instance.Play(AudioClipName.Button);
        Instantiate( Resources.Load("prefabs/store"));
    }
    public void ToSole()
    {
        AudioManager.Instance.Play(AudioClipName.Button);
        SceneManager.LoadScene("sole");
    }
    public void MissionBreafing(string descriptionString)
    {
        AudioManager.Instance.Play(AudioClipName.Button);
        mb = Instantiate(missionBreefing);
        mb.transform.SetParent(hud.transform, false);
        Text description = mb.transform.Find("description").GetComponent<Text>();
        description.text = descriptionString;
    }







}
