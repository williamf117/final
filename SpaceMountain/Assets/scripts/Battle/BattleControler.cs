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
        Vector3 playerSpawn = GameObject.Find("player spawn").transform.position;
        Vector3 eanamySpawn = GameObject.Find("eanamy spawn").transform.position;
        foreach (GameObject go in GameManager.instance.playerfleet) {
            if (go.name == "Frigate 1") {
                Fleet.Add((GameObject)Resources.Load("prefabs/Frigate 1"));
            }
            else
            {
                Fleet.Add((GameObject)Resources.Load("prefabs/playershipBase"));
            }
        }
        EanamyFleet = GameManager.instance.ships;
        float offset = 0;

        for(int i=0; i < Fleet.Count ; i++)
        {
            Fleet[i] = Instantiate(Fleet[i]);
            Fleet[i].transform.position = new Vector3(playerSpawn.x, playerSpawn.y + offset, 0);
            offset += 3;
            if (offset > 30)
            {
                offset = 0;
                playerSpawn.x += 5;
            }
        }
        offset = 0;
        for (int i = 0; i < EanamyFleet.Count; i++)
        {
            EanamyFleet[i] = Instantiate(EanamyFleet[i]);
            EanamyFleet[i].transform.position = new Vector3(eanamySpawn.x, eanamySpawn.y + offset, 0);
            offset += 5;
            if (offset > 15)
            {
                offset = 0;
                playerSpawn.x += 5;
            }
        }
        fin = new EndBattle();
        EventManager.AddleveBattleinvoker(this);
        
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Fleet.ToArray().Length == 0)
        {
          //  SceneManager.LoadScene("Menue");
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
          
            foreach (GameObject ship in Fleet)
            {
                if (ship.GetComponent<PlayerShip>().MouseOn)
                {
                    selected = ship.GetComponent<PlayerShip>();
                    Debug.Log("selected:"+selected);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            bool onEanamy = false;
            foreach (GameObject go in EanamyFleet) {
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
        //camra controles 
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
        float zoom = Input.GetAxisRaw("Mouse ScrollWheel") * 1;
        Camera.main.orthographicSize += zoom;
    }

    public void addEndBattleListener(UnityAction listener)
    {
        fin.AddListener(listener);
    }


}
