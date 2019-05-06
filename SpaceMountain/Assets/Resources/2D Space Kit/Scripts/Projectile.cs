﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public GameObject shoot_effect;
	public GameObject hit_effect;
	public GameObject firing_ship;
    [SerializeField]
    int damage = 20;
	// Use this for initialization
	void Start () {
		GameObject obj = (GameObject) Instantiate(shoot_effect, transform.position  - new Vector3(0,0,5), Quaternion.identity); //Spawn muzzle flash
		obj.transform.parent = firing_ship.transform;
		Destroy(gameObject, 5f); //Bullet will despawn after 5 seconds
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnTriggerEnter2D(Collider2D col) {
        Instantiate(hit_effect, transform.position, Quaternion.identity);
		//Don't want to collide with the ship that's shooting this thing, nor another projectile.
		if (col.gameObject != firing_ship && col.gameObject.tag != "bullet") {
			
            if (col.gameObject.GetComponent<Ship>()!=null)
            {
                col.gameObject.GetComponent<Ship>().Health -= damage;
                if (col.gameObject.GetComponent<Ship>().Health <= 0)
                {
                    Destroy(col.gameObject);
                }
            }
            AudioManager.Instance.Play(AudioClipName.explosion);
			Destroy(gameObject);
		}
	}
	
	
	
}
