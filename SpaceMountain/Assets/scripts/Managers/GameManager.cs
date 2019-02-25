using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<string> scenes;
    EventManager eventmanager;
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    [SerializeField]
    List<GameObject> ships = new List<GameObject>();//list of all ships that are in the game. 

    List<GameObject> playerfleet = new List<GameObject>();//list of all ships in the players fleet.
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
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Menue");
        }
    }

    public void changesene(string name) {
        try
        {
            //SceneManager.LoadScene("testBattle");
            SceneManager.LoadScene(name);
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
    





}
