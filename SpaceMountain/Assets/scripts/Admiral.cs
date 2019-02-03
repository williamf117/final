using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Admiral : Character
{
    List<Ship> fleet = new List<Ship>();
    Ship flagship;
    float strength, moral, x, y;

    List<Satellite> properties = new List<Satellite>();
    List<Character> minions = new List<Character>();
    float treasury, taxrate, popularity;
    public bool factionLeader = false;   


    float Upkeep
    {
        get { return strength; }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
