using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frigate : PlayerShip
{
    override protected int Index
    {

        get { return 1; }


    }
    float rainge = 10;
    private void Start()
    {
        Speed = 4;
        //maxSpeed = speed;
    }

    
      public override void FireOnTarget()
    {
        //calculate distence to target 
        Vector3 totarget = target.transform.position - transform.position;

        if (totarget.magnitude < rainge)
        {

            transform.RotateAround(target.transform.position, Vector3.forward, 10 * Time.deltaTime);
            // transform.LookAt(target.transform,Vector3.);
            // transform.Rotate(new Vector3(0, 0, 90));
            if (!oncooldown)
            {
                GameObject round = Instantiate(bullet, transform.GetChild(0).position, transform.rotation);
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), round.GetComponent<BoxCollider2D>());

                round = Instantiate(bullet, transform.GetChild(1).position,transform.rotation);
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), round.GetComponent<BoxCollider2D>());
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
