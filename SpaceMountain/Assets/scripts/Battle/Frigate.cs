﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frigate : PlayerShip
{
    List<PlayerControlledTurret> turrets = new List<PlayerControlledTurret>();
    //index for save script 
    override protected int Index
    {
        get { return 1; }
    }
    /// <summary>
    /// overide the price 
    /// </summary>
    public override float Price
    {
        get { return 1500; }
    }
    /// <summary>
    /// overide max helth for the ui scripts 
    /// </summary>
    public override float MaxHealth
    {

        get { return 150; }
    }
    
    float rainge = 10;
    /// <summary>
    /// use for initalization 
    /// </summary>
    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<PlayerControlledTurret>() != null)
            {
                turrets.Add(child.gameObject.GetComponent<PlayerControlledTurret>());
               
            }
        }
        foreach (PlayerControlledTurret t in turrets)
        {
            t.Shot_dammage = 15;
        }
        health = 150;
        maxHealth = health;
        Speed = 4;
        //maxSpeed = speed;
    }

    
      public override void FireOnTarget()
    {
        foreach (PlayerControlledTurret t in turrets)
        {
            t.Target(target);
        }
        //calculate distence to target 
        Vector3 totarget = target.transform.position - transform.position;

        if (totarget.magnitude < rainge)
        {
            Vector3 dir =( transform.position - target.transform.position).normalized;
          //  MoveToPosition(transform.position+ dir*10);
           
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
                    cooldown = 2;
                }
            }
        }
        else
        {//close rainge to target 

            Vector3 vectorToTarget = target.transform.position - transform.position;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = (desto - transform.position).normalized * speed;

            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);
        }
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            health -= 20;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.collider);
        }
    }
}
