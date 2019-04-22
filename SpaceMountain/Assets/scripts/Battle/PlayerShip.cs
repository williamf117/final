﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class PlayerShip : Ship
{
    protected Vector3 desto=Vector3.zero;
    protected GameObject target = null;
    protected float price = 1000;
   protected float cooldown = 2;
    [SerializeField]
   protected GameObject bullet;
   protected float speed =5;
    

    //an index to represent the type of ship for the save functions 
    protected virtual int Index {
        get { return 0; }
    }


    /// <summary>
    /// a way for the save game functrion to get the index of the ship
    /// </summary>
    public int GetIndex {
        get { return Index; }
    }




    public float Price
    {
        get { return price; }
    }

    public float Speed
    {
        set { speed = value; }
    }



   


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(speed);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 vectorToTarget = desto - transform.position;
        if (desto != Vector3.zero)
        {
            //turn to face target
           // speed = maxSpeed;
            rb.velocity = (desto - transform.position).normalized *speed;

            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);



        }

        if (target != null)
        {

             vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);

            FireOnTarget();
        
        }
        
    }
    /// <summary>
    /// a function to set the desto for the ship.
    /// </summary>
    /// <param name="position"></param>
    public void MoveToPosition(Vector3 position)
    {
        desto = position;
       
        //Debug.Log("CALLED");
    }
  
   public virtual void FireOnTarget()
    {
        if (!oncooldown)
        {
            GameObject round = Instantiate(bullet, transform.position, transform.rotation);
            oncooldown = true;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), round.GetComponent<BoxCollider2D>());


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
        if (collision.gameObject.tag == "PlayerShip")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    public void setTarget(GameObject t)
    {
        desto = Vector3.zero;
        target = t;
    }

    
}
