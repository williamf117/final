using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class PlayerShip : Ship
{
   Vector3 desto=Vector3.zero;
    GameObject target = null;
    
    float cooldown = 2;
    [SerializeField]
    GameObject bullet;
    

   



 

    // Update is called once per frame
    void Update()
    {
        if (desto != Vector3.zero)
        {
            //turn to face target

            Vector3 vectorToTarget = desto - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);

            //move to postion 
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(desto.x,desto.y,-1), .01f);
            
        }

        if (target != null)
        {

            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);

            FireOnTarget();
            LineRenderer lr = GetComponent<LineRenderer>();
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1,target.transform.position);
            lr.startColor=Color.red;
            lr.endColor = Color.red;

        }
        else
        {
            GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
            GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
        }
    }
    public void MoveToPosition(Vector3 position)
    {
        desto = position;
       
        //Debug.Log("CALLED");
    }
    //void OnMouseOver()
    //{
        
    //    mouseon = true;
    //    gameObject.GetComponent<SpriteRenderer>().sprite = Hilight;

    //}

    //void OnMouseExit()
    //{
    //    gameObject.GetComponent<SpriteRenderer>().sprite = noHilight;
    //    mouseon = false;
    //}
    void FireOnTarget()
    {
        if (!oncooldown)
        {
            GameObject round = Instantiate(bullet, transform.position, transform.rotation);
            oncooldown = true;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), round.GetComponent<CircleCollider2D>());


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
            if(health<=0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void setTarget(GameObject t)
    {
        desto = Vector3.zero;
        target = t;
    }

    
}
