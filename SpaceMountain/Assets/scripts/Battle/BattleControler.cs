using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleControler : MonoBehaviour
{
    List<GameObject> Fleet = new List<GameObject>();
    List<GameObject> EanamyFleet  = new List<GameObject>();
    PlayerShip selected;

   
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] fleet;
        GameObject[] eanamyFleet;
        fleet = GameObject.FindGameObjectsWithTag("PlayerShip");
        eanamyFleet = GameObject.FindGameObjectsWithTag("HostileFleet");

        foreach(GameObject go in fleet)
        {
            Fleet.Add(go);
        }
        foreach(GameObject go in eanamyFleet)
        {
            EanamyFleet.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // remove destroyed ships from the lists 
        foreach(GameObject go in Fleet.ToArray())
        {
            if (go == null)
            {
                Fleet.Remove(go);
            }
        }
        foreach (GameObject go in EanamyFleet.ToArray())
        {
            if (go == null)
            {
                EanamyFleet.Remove(go);
            }
        }


        //player controles 
        if (Input.GetMouseButtonDown(0))
        {
          
            foreach (GameObject ship in Fleet.ToArray())
            {
                if (ship.GetComponent<PlayerShip>().MouseOn)
                {
                    selected = ship.GetComponent<PlayerShip>();
                    Debug.Log(selected);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            bool onEanamy = false;
            foreach (GameObject go in EanamyFleet.ToArray()) {
                onEanamy = false;
                if (go.GetComponent<EanamyBase>().MouseOn)
                {
                    onEanamy = true;
                }
                if (onEanamy)
                  {
                    selected.GetComponent<PlayerShip>().setTarget(go);
                    Debug.Log("on target");
                  }
            }

            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (selected != null && !onEanamy)
            {
                selected.GetComponent<PlayerShip>().MoveToPosition(mouse);
            }
            
        }

    }


}
