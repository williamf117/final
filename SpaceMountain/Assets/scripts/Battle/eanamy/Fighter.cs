using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : EanamyBase
{

    int missils = 2;
    GameObject MotherShip;
    bool returning = false;
    [SerializeField]
    GameObject missle;
    int misslespeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        maxrainge = 10;
        rb2d = GetComponent<Rigidbody2D>();
        moveSpeed = 10;
        cooldown = .5f;
        health = 21;
    }

    // Update is called once per frame
    void Update()
    {
        //check if we are returning or the target was destroyed
        if (!returning && currenttarget!=null)
        {
            Vector3 totarget= currenttarget.transform.position - transform.position;
            if (totarget.magnitude < maxrainge&&missils>0)
            {
                fireOnTarget();
            }
            else if (missils == 0)//are we out of ammo 
            {
                returning = true;
            }
            else//we must be out of rainge get closer 
            {
                Vector3 vectorToTarget = currenttarget.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);
                rb2d.velocity = vectorToTarget.normalized * moveSpeed;

            }
        }
        if (returning)
        {
            try
            {
                Vector3 vectorToTarget = MotherShip.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);
                rb2d.velocity = vectorToTarget.normalized * moveSpeed;
                if (vectorToTarget.magnitude < 5)
                {
                    Destroy(gameObject);
                }
            }
            catch
            {
                Destroy(gameObject);
            }

        }
    }
    protected override void fireOnTarget()
    {
        if (!oncooldown)//shoot the missles 
        {
            GameObject m = Instantiate(missle);
            m.transform.position = transform.position;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), m.GetComponent<BoxCollider2D>());
            m.GetComponent<Rigidbody2D>().velocity= (currenttarget.transform.position - transform.position).normalized*misslespeed;
            missils--;
            m.GetComponent<Projectile>().firing_ship = gameObject;
        }
        if (oncooldown)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                oncooldown = false;
                cooldown = .5f;
            }
        }
    }
    /// <summary>
    /// get the target that you need to hit 
    /// </summary>
    /// <param name="t"></param>
    public void setTarget(GameObject t)
    {
        currenttarget = t;
    }
    /// <summary>
    /// set the carrier that you should return to 
    /// </summary>
    /// <param name="go"></param>
    public void SetMotherShip(GameObject go)
    {
        MotherShip = go;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "HostileFleet" || collision.gameObject.tag == "PlayerShip")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
