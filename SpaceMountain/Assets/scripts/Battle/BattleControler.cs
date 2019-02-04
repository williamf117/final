using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleControler : MonoBehaviour
{
    GameObject[] Fleet;
    PlayerShip selected;

    GameObject[] EanamyFleet;
    // Start is called before the first frame update
    void Start()
    {
        Fleet = GameObject.FindGameObjectsWithTag("PlayerShip");
        EanamyFleet = GameObject.FindGameObjectsWithTag("HostileFleet");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          
            foreach (GameObject ship in Fleet)
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
            foreach (GameObject go in EanamyFleet) {

                if (go.GetComponent<railgun>().MouseOn)
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
