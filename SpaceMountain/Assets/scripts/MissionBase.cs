using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBase : MonoBehaviour
{
    protected InGameMenue drawmission;
    protected missionType type = missionType.kill;
    protected  List<GameObject> encounters = new List<GameObject>();
    
   protected GameObject desto;
    protected string description ;
   public int reward;
   
    [SerializeField]
    protected GameObject PrefabFleet;
    public bool completed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Print()
    {

        drawmission.MissionBreafing(description + " reward:" + reward);
    }
    public void compleatMission()
    {
        completed = true;
    }
}
