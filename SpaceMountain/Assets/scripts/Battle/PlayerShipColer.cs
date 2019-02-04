using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerShipColer : MonoBehaviour
{
    GameObject[] Fleet;
    PlayerShip selected; 
    // Start is called before the first frame update
    void Start()
    {
        Fleet = GameObject.FindGameObjectsWithTag("PlayerShip");
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
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (selected != null)
            {
                selected.GetComponent<PlayerShip>().MoveToPosition(mouse);
            }
        }

    }


}
