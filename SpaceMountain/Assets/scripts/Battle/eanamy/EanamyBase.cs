﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EanamyBase : Ship
{
    [SerializeField]
    protected float maxrainge = 15, minrainge = 5, moveSpeed=.5f, cooldown=1;//effective rainge of the gun. 
    [SerializeField]
    GameObject bullet;
   
    List<GameObject> playerships=new List<GameObject>();
   protected GameObject currenttarget;

    public enum states {
        CloseRainge,
        attack,
        openRainge

    }


  

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (currenttarget == null)
        {
            currenttarget = FindTarget();
        }
        else
        {
            float rainge = Vector2.Distance(transform.position, currenttarget.transform.position);


            if (rainge > minrainge && rainge < maxrainge)
            {
                inrainge = true;
                fireOnTarget();
            }
            else if (rainge < minrainge && rainge < maxrainge)
            {
                inrainge = false;
                OpenRainge();
            }
            else if (rainge > maxrainge)
            {
                inrainge = false;
                CloseToRaing();
            }


            Vector3 vectorToTarget = currenttarget.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);

            //rotate to face the target if inrainge
           
        }
    }

 protected  virtual void fireOnTarget()
    {
      
            //turn to face target
            rb2d.velocity = Vector2.zero;
            

            //fire if you can
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


    /// <summary>
    /// find the closes ship and set it as the target 
    /// </summary>
    /// <returns></returns>
    GameObject FindTarget()
    {
        try
        {
            playerships = Camera.main.GetComponent<BattleControler>().Playerships;
            if (playerships.Count > 0)
            {
                GameObject target = null;
                float closest = Mathf.Infinity;
                foreach (GameObject go in playerships)
                {
                    //calculate rainge if it is less than closest set it to be closest
                    float rainge = Mathf.Abs((transform.position - go.transform.position).magnitude);
                    if (rainge < closest)
                    {
                        closest = rainge;
                        target = go;

                    }
                }
                return target;
                //Debug.Log(playerships[target]);

            }

        }
        catch
        {

            return null;
        }
        return null;
    }

    /// <summary>
    /// a function to close to rainge 
    /// </summary>
    void CloseToRaing()
    {
        Vector2 vectorToTarget = currenttarget.transform.position - transform.position;
       //  transform.position = Vector3.MoveTowards(transform.position, currenttarget.transform.position, Time.deltaTime * moveSpeed);
       rb2d.velocity = vectorToTarget.normalized*moveSpeed;
     
    }
    /// <summary>
    /// a function to open rainge 
    /// </summary>
    void OpenRainge()
    {
        Vector2 vectorToTarget = currenttarget.transform.position - transform.position;
        rb2d.velocity = -vectorToTarget.normalized * moveSpeed;
    }

    //void OnMouseOver()
    //{
    //    //If your mouse hovers over the GameObject with the script attached, output this message
    //    mouseon = true;
    //}

    //void OnMouseExit()
    //{
    //    //The mouse is no longer hovering over the GameObject so output this message each frame
    //    mouseon = false;
    //}
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
        if (collision.gameObject.tag == "Projectile")
        {
            health -= 30;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "HostileFleet"|| collision.gameObject.tag == "PlayerShip")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
