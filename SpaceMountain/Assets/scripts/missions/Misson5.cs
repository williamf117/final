using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class Misson5 : MissionBase
{
    int fleetspawnd = 0;
    float spawincooldown = 10;
    bool BossSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        description = "The pirate lord has placed a massive bounty on your head fleets from across the system are looking for you. You wont survive long with this kind of bounty. If you can destroy one of their carrier groups then they might decide that you are more trouble than your worth. there might be one near satern";
        reward = 100000;
        

    }

    // Update is called once per frame
    void Update()
    {
        EventManager.AddMissionCompleatInvokers(this);
        drawmission = GameObject.FindGameObjectWithTag("HUD").GetComponent<InGameMenue>();
        if (SceneManager.GetActiveScene().name == "saturn" &&BossSpawned==false)
        {

            GameObject f = Instantiate((GameObject)Resources.Load("prefabs/Prefabfleet"));
            f.GetComponent<Fleet>().MakeFleet(8);
            f.GetComponent<Fleet>().AddShip((GameObject)Resources.Load("prefabs/Carrier 1"));
            f.GetComponent<Fleet>().Target=GameObject.Find("Saturn");
            BossSpawned = true;

        }
        if(SceneManager.GetActiveScene().name == "saturn"&& BossSpawned)
        {
            EventManager.AddEndbattlelistener(EndMission);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        //grap all the satelights 
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("Satellite");
        
        foreach(GameObject go in spawnpoints)
        {
            int rand = Random.Range(1, 15);
            if (rand > 3)
            {
                continue;
            }
            else if (fleetspawnd<3)
            {
                GameObject f = Instantiate((GameObject) Resources.Load("prefabs/Prefabfleet"));
                f.transform.position = go.transform.position;
                f.GetComponent<Fleet>().MakeFleet(Random.Range(2, 5));
                f.GetComponent<Fleet>().Target = GameObject.FindGameObjectWithTag("Player");
                
                fleetspawnd++;
            }
        }


    }
    public void EndMission()
    {
        completed = true;
        missioncomplete.Invoke("Mission 5 complete");
    }
}
