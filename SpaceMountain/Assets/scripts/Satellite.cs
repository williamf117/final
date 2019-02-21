﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// a class to controle all satelights 
/// </summary>
public class Satellite : MonoBehaviour
{
    GameObject sun;
   // Admiral governor;
    [SerializeField]
    float year = 1, growth = 1;
    enterPlanetEvent enterplanet;

    
    // Use this for initialization
    void Start()
    {
        enterplanet = new enterPlanetEvent();
        EventManager.AddNewEnterPlanetInvoker(this);
        sun = transform.parent.gameObject;

        Sprite s = GetComponent<SpriteRenderer>().sprite;

        GameObject go = new GameObject();
        go.AddComponent<SpriteRenderer>().sprite = s;
        go.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        go.transform.localScale = new Vector3(10, 10, -1);
        //  Instantiate(go);
        go.transform.parent = gameObject.transform;
        go.layer = 9;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enterplanet.Invoke(this.gameObject.name);
        }
    }
    private void FixedUpdate()
    {
        transform.RotateAround(sun.transform.position, Vector3.forward, year);
    }
    public void AddEnterPlanetListener(UnityAction<string> listener) {
        enterplanet.AddListener(listener); 
    }

}
