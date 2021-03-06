﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;



public class Ship : MonoBehaviour {

    protected bool inrainge = false;
    protected bool oncooldown = false;
    protected float health = 100;
    protected float rotationspeed = 3;
   protected  bool mouseon = false;
   protected float maxHealth=100;
   
    //[SerializeField]
    //Sprite Hilight;
    Sprite noHilight;
   protected Rigidbody2D rb2d;
    public bool MouseOn
    {
        get { return mouseon; }
    }

    public virtual float MaxHealth {

        get { return maxHealth; }
    }
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    // Use this for initialization
   protected void Awake () {
        maxHealth = health;
        noHilight = GetComponentInChildren<SpriteRenderer>().sprite;
        //Debug.Log(noHilight.name);
        rb2d = GetComponent<Rigidbody2D>();
        Sprite s = GetComponent<SpriteRenderer>().sprite;

        GameObject go = new GameObject();
        go.AddComponent<SpriteRenderer>().sprite = s;
        go.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        go.transform.localScale = new Vector3(3, 3, -1);
       //  Instantiate(go);
        go.transform.parent = gameObject.transform;
        go.layer = 9;

        if (GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseOver()
    {

        mouseon = true;
        //gameObject.GetComponent<SpriteRenderer>().sprite = Hilight;
        ///Debug.Log(mouseon);

    }

    void OnMouseExit()
    {
       // gameObject.GetComponent<SpriteRenderer>().sprite = noHilight;
        mouseon = false;
    }

}
