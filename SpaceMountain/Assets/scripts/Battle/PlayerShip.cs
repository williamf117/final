using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
   Vector3 desto=Vector3.zero;
    GameObject target = null;
    float rotationspeed = 3;
    float health = 100;
    bool oncooldown = false;
    float cooldown = 2;
    [SerializeField]
    GameObject bullet;

    bool mouseon;

    public bool MouseOn
    {
        get { return mouseon; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        }
    }
    public void MoveToPosition(Vector3 position)
    {
        desto = position;
       
        //Debug.Log("CALLED");
    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        mouseon = true;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        mouseon = false;
    }
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
