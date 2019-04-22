using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : PlayerShip
{
    override protected int Index
    {

        get { return 3; }


    }
    List<PlayerControlledTurret> turrets = new List<PlayerControlledTurret>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<PlayerControlledTurret>() != null)
            {
                turrets.Add(child.gameObject.GetComponent<PlayerControlledTurret>());
            }
        }
        //set variables 
        speed = 2;
       
        rotationspeed = .5f;
        health = 500;
       // maxSpeed = speed;
    }
    public override void FireOnTarget()
    {
        //calculate distence to target 
        Vector3 totarget = target.transform.position - transform.position;
        foreach(PlayerControlledTurret t in turrets)
        {
            t.Target(target);
        }
        if (totarget.magnitude < 20)
        {

           
            // transform.LookAt(target.transform,Vector3.);
            // transform.Rotate(new Vector3(0, 0, 90));
            if (!oncooldown)
            {
                foreach (PlayerControlledTurret t in turrets)
                {
                    t.Fire();
                }
                oncooldown = true;
                Vector3 vectorToTarget = target.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);
            }
            if (oncooldown)
            {
                cooldown -= Time.deltaTime;
                if (cooldown < 0)
                {
                    oncooldown = false;
                    cooldown = 1;
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, -1), speed);
        }

    }
    
}
