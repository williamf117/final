using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class BattleControler : MonoBehaviour
{
    List<GameObject> Fleet = new List<GameObject>();
    List<GameObject> EanamyFleet  = new List<GameObject>();
    PlayerShip selected;
    EndBattle fin;

   
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] fleet;
        GameObject[] eanamyFleet;
        fleet = GameObject.FindGameObjectsWithTag("PlayerShip");
        eanamyFleet = GameObject.FindGameObjectsWithTag("HostileFleet");
        fin = new EndBattle();
        EventManager.AddleveBattleinvoker(this);
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

        if (Fleet.ToArray().Length == 0)
        {
            SceneManager.LoadScene("Menue");
        }
        if (EanamyFleet.ToArray().Length == 0)
        {
            // GameManager.
            fin.Invoke();
        }

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
    private void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetAxis("Horizontal") != 0)
        {
            move.x = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            move.y = Input.GetAxis("Vertical");
        }
        move.z = 0;
        move = move.normalized*.25f;
        gameObject.transform.position = gameObject.transform.position + move;
       
    }

    public void addEndBattleListener(UnityAction listener)
    {
        fin.AddListener(listener);
    }


}
