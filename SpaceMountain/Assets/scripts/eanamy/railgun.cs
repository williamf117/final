using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class railgun : MonoBehaviour
{
    [SerializeField]
    float maxrainge = 15, minrainge = 5, moveSpeed, cooldown=3;//effective rainge of the gun. 
    [SerializeField]
    GameObject bullet;
    Rigidbody2D rb2d;
    GameObject[] playerships;
    GameObject currenttarget; 
    float rotationspeed=3;
    bool inrainge=false;
    bool oncooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currenttarget == null)
        {
            currenttarget = FindTarget();
        }
        float rainge = Vector2.Distance(transform.position, currenttarget.transform.position);
     

        if (rainge > minrainge && rainge<maxrainge)
        {
            inrainge = true;
        }
        else if(rainge < minrainge&&rainge<maxrainge)
        {
            inrainge = false;
            OpenRainge();
        }
        else if(rainge > maxrainge)
        {
            inrainge = false;
            CloseToRaing();
        }

       


        //rotate to face the target if inrainge
        if (inrainge)
        {
            //turn to face target
            rb2d.velocity = Vector2.zero;
            Vector3 vectorToTarget = currenttarget.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);

            //fire if you can
            if (!oncooldown)
            {
               GameObject round= Instantiate(bullet, transform.position, transform.rotation);
                oncooldown = true;
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),round.GetComponent<CircleCollider2D>());


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

    GameObject FindTarget()
    {
        playerships = GameObject.FindGameObjectsWithTag("PlayerShip");
        int target= Random.Range(0, playerships.Length - 1);
        Debug.Log(playerships[target]);
        return playerships[target];
        
    }

    /// <summary>
    /// a function to close to rainge 
    /// </summary>
    void CloseToRaing()
    {
        Vector2 vectorToTarget = currenttarget.transform.position - transform.position;
        // transform.position = Vector3.MoveTowards(transform.position, currenttarget.transform.position, Time.deltaTime * moveSpeed);
        rb2d.velocity = vectorToTarget;
     
    }
    /// <summary>
    /// a function to open rainge 
    /// </summary>
    void OpenRainge()
    {

    }
}
