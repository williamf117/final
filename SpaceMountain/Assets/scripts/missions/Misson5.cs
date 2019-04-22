using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Misson5 : MissionBase
{
    int fleetspawnd = 0;
    // Start is called before the first frame update
    void Start()
    {
        description = "The pirate lord has placed a massive bounty on your head fleets from across the system are looking for you. You wont survive long with this kind of bounty. If you can destroy one of their carrier groups then they might decide that you are more trouble than your worth. there might be one near satern";
        reward = 100000;
        

    }

    // Update is called once per frame
    void Update()
    {

     
        
    }
    private void OnLevelWasLoaded(int level)
    {
        //grap all the satelights 
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("Satellite");
        
        foreach(GameObject go in spawnpoints)
        {
            int rand = Random.Range(1, 8);
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
}
