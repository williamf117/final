using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<string> scenes;
    EventManager eventmanager;
    // Start is called before the first frame update
    void Start()
    {
        //eventmanager = new EventManager();
        EventManager.AddNewenterPlanetListener(changesene);
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
