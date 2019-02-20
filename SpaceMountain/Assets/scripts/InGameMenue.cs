using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenue : MonoBehaviour
{
    [SerializeField]
    HUD navMenue;
    [SerializeField]
    Text funds;
    Player playerscript; 
    // Start is called before the first frame update
    void Start()
    {
        playerscript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void navigation()
    {
        navMenue.CreateMenue();
    }
    public void Mission()
    {
        Debug.Log("mission");

    }
    public void Fleet()
    {
        Debug.Log("fleet");
    }
    public void ToSole()
    {
        Camera.main.GetComponent<GameManager>().changesene("sole");
    }
}
