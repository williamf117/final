using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionBase : MonoBehaviour
{
    protected InGameMenue drawmission;
    protected missionType type = missionType.kill;
    protected  List<GameObject> encounters = new List<GameObject>();
    protected MissionCompleateEvent missioncomplete;
    private void Awake()
    {
        missioncomplete = new MissionCompleateEvent();
        drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        prefabFleet = (GameObject)Resources.Load("prefabs/Prefabfleet");
    }

    protected GameObject desto;
    protected string description ;
   public int reward;
    [SerializeField]
    protected GameObject prefabFleet;
    
    public bool completed = false;
    // Start is called before the first frame update



    public virtual void Print()
    {

        drawmission.MissionBreafing(description + " reward:" + reward);
    }
    public void compleatMission()
    {
        //completed = true;
    }
    public void AddMissionCompleteListener(UnityAction<string> listener)
    {
        missioncomplete.AddListener(listener);
    }
}
