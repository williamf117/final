using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EanamyBase : Ship
{
    [SerializeField]
    float maxrainge = 15, minrainge = 5, moveSpeed=.5f, cooldown=3;//effective rainge of the gun. 
    [SerializeField]
    GameObject bullet;
   
    GameObject[] playerships;
    GameObject currenttarget; 
   
   

  

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
            if (inrainge)
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
                        cooldown = 3;
                    }
                }
            }
        }
    }

    GameObject FindTarget()
    {
        playerships = GameObject.FindGameObjectsWithTag("PlayerShip");
        if (playerships.Length > 0) { 
        int target = Random.Range(0, playerships.Length - 1);
        //Debug.Log(playerships[target]);
        return playerships[target];
        }
        else
        {
            return null;
        }

        
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
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
