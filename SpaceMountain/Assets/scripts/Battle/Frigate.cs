using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frigate : PlayerShip
{
    float rainge = 10;
    private void Start()
    {
        Speed = .05f;
    }

    
      public override void FireOnTarget()
    {
        //calculate distence to target 
        Vector3 totarget = target.transform.position - transform.position;

        if (totarget.magnitude < 10)
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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, -1), speed);
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
