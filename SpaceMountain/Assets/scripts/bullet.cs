using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    float x;                // x and y coordinates for the bullet movement 
    float y;
    float bulletSpeed = .5f;  //speed of the bullet
    float removetime = 2;   //time until the bullet is removed 
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //get the rigid body 
        x = Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z) * bulletSpeed; //determine x and y components
        y = Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z) * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //move the bullet appropriately
        rb2d.MovePosition(new Vector2(transform.position.x + x, transform.position.y + y));

        removetime -= Time.deltaTime;

        //delete the bullet after a certain time 
        if (removetime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
