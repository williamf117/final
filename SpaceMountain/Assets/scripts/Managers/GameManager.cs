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
    [SerializeField]
    List<string> scenes;
    EventManager eventmanager;
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    [SerializeField]
    List<GameObject> ships = new List<GameObject>();//list of all ships that are in the game. 
    string lastscene;
    List<GameObject> playerfleet = new List<GameObject>();//list of all ships in the players fleet.
    state currsate = state.map;
    float playerfunds;
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
        //eventmanager = new EventManager();
        EventManager.AddNewenterPlanetListener(changesene);
        EventManager.AddNewEnterBattleListener(startBattle);
        EventManager.AddEndbattlelistener(endbattle);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Menue");
        }
    }
    /// <summary>
    /// handle entering a planets scean in an event
    /// </summary>
    /// <param name="name"></param>
    public void changesene(string name) {
        try
        {
            //SceneManager.LoadScene("testBattle");
            SceneManager.LoadScene(name);
            if (SceneManager.GetActiveScene().name == "Jupiter" && GetComponent < MissionBase >()== null )
            {
                gameObject.AddComponent<MissionBase>();
            }
        }
        catch
        {
            Debug.Log("no scene");
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
        lastscene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("testBattle");
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

}
