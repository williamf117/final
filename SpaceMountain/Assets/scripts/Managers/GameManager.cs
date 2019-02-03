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
        
    }

    void changesene(string name) {
        SceneManager.LoadScene(name);
    }



}
