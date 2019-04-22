using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class BattleControler : MonoBehaviour
{
    List<GameObject> Fleet = new List<GameObject>();
    List<GameObject> EanamyFleet = new List<GameObject>();
    PlayerShip selected;
    EndBattle fin;

    public PlayerShip SetActive
    {
        get { return selected; }
        set { selected = value; }
    }



    public List<GameObject> Playerships{
        get { return Fleet; }
        }
   
    // Start is called before the first frame update
    void Start()
    {
        //stop the map music and play the battle music 
        AudioManager.Instance.StopSource(AudioClipName.SpaceLoop);
        AudioManager.Instance.Play(AudioClipName.BattleLoop);
        //find the spawn points 
        Vector3 playerSpawn = GameObject.Find("player spawn").transform.position;
        Vector3 eanamySpawn = GameObject.Find("eanamy spawn").transform.position;
        //spawin the player fleet using a deep copy 
        foreach (GameObject go in GameManager.instance.playerfleet) {
            if (go.name == "Frigate 1") {
                Fleet.Add((GameObject)Resources.Load("prefabs/Frigate 1"));
            }
            else if (go.name == "playershipBase")
            {
                Fleet.Add((GameObject)Resources.Load("prefabs/playershipBase"));
            }
            else if(go.name == "Titan")
            {
                Fleet.Add((GameObject)Resources.Load("prefabs/Titan"));
            }
            else if (go.name == "PlayerCruiser")
            {
                Fleet.Add((GameObject)Resources.Load("prefabs/PlayerCruiser"));
            }
        }
        //spawn the eanamy fleet 
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
                playerSpawn.x += 30;
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
        GameObject.FindGameObjectWithTag("HUD").GetComponent<battleUI>().initalize();
       
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
            //end the battle music and go over to the map music 
            AudioManager.Instance.StopSource(AudioClipName.BattleLoop);
            AudioManager.Instance.Play(AudioClipName.SpaceLoop);
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
            //check to see if we are clicking on a ship
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
                //check to see if we are clicking on a ship 
                
                if ((go.GetComponent<EanamyBase>().MouseOn))
                  {
                    selected.GetComponent<PlayerShip>().setTarget(go);
                  //  selected.GetComponent<PlayerShip>().MoveToPosition(Vector3.zero);
                    Debug.Log("on target");
                    onEanamy = true;
                  }
            }

            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (selected != null && !onEanamy)
            {
                //selected.GetComponent<PlayerShip>().setTarget(null);
                selected.GetComponent<PlayerShip>().MoveToPosition(mouse);
            }
            
        }

        //cheat controle
        if (Input.GetKey(KeyCode.Z))
        {
            foreach(GameObject go in EanamyFleet)
            {
                Destroy(go);
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
