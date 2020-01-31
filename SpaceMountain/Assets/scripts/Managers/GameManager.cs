using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
enum state
{
    battle,
    map
}

public class GameManager : MonoBehaviour
{
    //list of all scenes in the game 
    [SerializeField]
    List<string> scenes;
    //refrence the event manager 
    EventManager eventmanager;
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    [SerializeField]
    public List<GameObject> ships = new List<GameObject>();//list of all ships that are inthe eanamy fleet 
    string lastscene;
    public List<GameObject> playerfleet = new List<GameObject>();//list of all ships in the players fleet.
    public List<GameObject> playableShips = new List<GameObject>();//list of all playable ships for the store. 
    state currsate = state.map;
    MissionBase currMission;
    Satellite locationLocal, locationGlobal;
    int misioncount = 1;

    string TargetBattle = "testBattle";

    public string BattleSceneName
    {
        get { return TargetBattle; }
        set { TargetBattle = value; }
    }

    //a way to keep track of missions 
    public int MissionCount
    {
        get { return misioncount; }
    }

    //public acssesor for player fleet
    public List<GameObject> PlayerFleet{
        get { return playerfleet; }
        set { playerfleet = value; }
        }
   
    /// <summary>
    /// puclic acssesor for the currenr mission
    /// </summary>
    public MissionBase CurrMission
    {
        get { return currMission; }
        set { currMission = value; }
    }

    float playerfunds=500;
    public float Funds
    {
        get { return playerfunds; }
        set { playerfunds = value; }
    }
   
    

    //Awake is always called before any Start functions
    void Awake()
    {
        
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

        // Start is called before the first frame update
        void Start()
    {
        //add all the events needed to the list 
        EventManager.AddNewenterPlanetListener(changesene);
        EventManager.AddNewEnterBattleListener(startBattle);
        EventManager.AddEndbattlelistener(endbattle);

        AudioManager.Initialize(gameObject.GetComponent<AudioSource>());
        
    }

    // Update is called once per frame
    void Update()
    {

       
        if (currMission!=null && currMission.completed)
        {
            Funds += currMission.reward;
            Destroy(currMission);
        }

        if (SceneManager.GetActiveScene().name == "Menue") {
            Destroy(currMission);
            misioncount = 10;
           // Destroy(gameObject);
        }
        //get the next mission loded
        if (currMission == null && SceneManager.GetActiveScene().name != "Menue") {
            switch (misioncount)
            {
                case 1:
                    currMission = gameObject.AddComponent<Mission1>();
                    misioncount++;
                    break;
                case 2:
                    currMission = gameObject.AddComponent<Mission2>();
                    misioncount++;
                    break;
                case 3:
                    currMission = gameObject.AddComponent<Mission3>();
                    misioncount++;
                    break;
                case 4:
                    currMission = gameObject.AddComponent<Mission4>();
                    misioncount++;
                    break;
                case 5:
                    currMission =gameObject.AddComponent<Misson5>();
                    misioncount++;
                    break;
                case 6:
                    currMission= gameObject.AddComponent<Mission6>();
                    misioncount++;
                    break;
                case 7:
                    currMission = gameObject.AddComponent<Mission7>();
                    misioncount++;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Instantiate((GameObject)Resources.Load("prefabs/Pause"));
        }
    }
    /// <summary>
    /// handle entering a planets scean in an event
    /// </summary>
    /// <param name="name"></param>
    public void changesene(string name) {

        if (name != "mercury" || name != "venus")
        {
            try
            {
                if(name=="testBattle"||name=="BaseBattle")
                {
                    AudioManager.Instance.StopSource(AudioClipName.SpaceLoop);
                    AudioManager.Instance.Play(AudioClipName.BattleLoop);
                }
                //SceneManager.LoadScene("testBattle");
                SceneManager.LoadScene(name);

            }
            catch
            {
                Debug.Log("no scene");
            }
        }
    }
    public void quit()
    {
        Application.Quit();
    }
    /// <summary>
    /// handle the start of a battle in an event 
    /// </summary>
    /// <param name="fleet"></param>
    public void startBattle(List<GameObject> fleet)
    {
       // playerfleet=
        ships = fleet;
        lastscene = SceneManager.GetActiveScene().name;
        
        
        SceneManager.LoadScene(TargetBattle);
        currsate = state.battle;
        Vector2 pos = Vector2.zero;
        foreach(GameObject go in fleet)
        {
            Instantiate(go);
        }
    }
 


    /// <summary>
    /// end the battle and go back to the last scean 
    /// </summary>
    public void endbattle()
    {
        currsate = state.map;
        SceneManager.LoadScene(lastscene);
    }
    //public void AddPlayerShip(GameObject ship)
    //{
    //    playerfleet.Add(ship);
    //}

}
