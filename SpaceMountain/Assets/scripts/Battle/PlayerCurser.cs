using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurser : PlayerShip
{
     override protected int Index
    {

        get { return 2; }


    }
    public override float Price
    {
        get { return 3000; }
    }
    /// <summary>
    /// overide max helth for the ui scripts 
    /// </summary>
    public override float MaxHealth
    {

        get { return 200; }
    }

    //list for all turrets 
    List<PlayerControlledTurret> turrets = new List<PlayerControlledTurret>();
    bool justInrainge = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 4;
        price = 3000;
      //  maxSpeed = speed;
        rotationspeed = 2.5f;
        health = 200;
        maxHealth = health;
        //populate the list of turrets 
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<PlayerControlledTurret>() != null)
            {
                turrets.Add(child.gameObject.GetComponent<PlayerControlledTurret>());
            }
        }
    }

    public override void FireOnTarget()
    {
       
        //calculate distence to target 
        Vector3 totarget = target.transform.position - transform.position;
        foreach (PlayerControlledTurret t in turrets)
        {
            t.Target(target);
        }
        if (totarget.magnitude < 10 )
        {

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            float desieredangle=90;
            if (!justInrainge)
            {
                //find the rotation needed to face the object 
                float angletoTarget = Mathf.Tan(totarget.y / totarget.x);
                angletoTarget = Mathf.Rad2Deg * angletoTarget;
                 desieredangle = angletoTarget + 90;
                justInrainge = true;
            }
            //rotate twards that 
            Vector3 to = new Vector3(0, 0, desieredangle);
            if (Vector3.Distance(transform.eulerAngles, to) > 0.1f)
            {
                transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, .25f* Time.deltaTime);
            }
            else
            {
                transform.eulerAngles = to;
               
            }

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
                float angletoTar = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angletoTar, Vector3.forward);
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
            speed = 4;
            justInrainge = false;
            GetComponent<Rigidbody2D>().velocity = totarget.normalized * speed;
        }
    }
}
