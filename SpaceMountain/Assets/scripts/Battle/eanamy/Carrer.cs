using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrer : EanamyBase
{
    int maxfightewrs = 9;
    [SerializeField]
    GameObject fighter;
    List<GameObject> fighters = new List<GameObject>();
    GameObject launchpos;
    // Start is called before the first frame update
    void Start()
    {
        maxrainge = 200;
        health = 500;
        maxHealth = 500;
        moveSpeed = 1;
        cooldown = 10;
        launchpos = transform.GetChild(0).gameObject;

    }

    // Update is called once per frame

    protected override void fireOnTarget()
    {
        if (!oncooldown)
        {
            //launch a fighter 
            GameObject go= Instantiate(fighter);
            go.transform.position = launchpos.transform.position;
            fighters.Add(go);
            go.GetComponent<Fighter>().setTarget(currenttarget);
            go.GetComponent<Fighter>().SetMotherShip(gameObject);
            oncooldown = true;

            
        }
        if (oncooldown)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                oncooldown = false;
                cooldown = 10;
            }
        }
    }
}
