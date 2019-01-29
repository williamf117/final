using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {
    [SerializeField]
    float treasury = 1000, reputation, tax=1;
    List<Satellite> settlements;
    List<Character> characters, admirals;

    //float Expenses { get; }
    //float Income { get; }

    void MyUpdate()
    {
        foreach(Satellite s in settlements)
        {
            treasury += s.EconomicUpdate(tax);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
