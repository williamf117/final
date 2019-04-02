﻿using UnityEngine;
using System.Collections;

public class PlayerControlledTurret : MonoBehaviour {

	public GameObject weapon_prefab;
	public GameObject[] barrel_hardpoints;
	public float turret_rotation_speed = 3f;
	public float shot_speed;
	int barrel_index = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Target (GameObject target) {
	
		//This makes the turret aim at the mouse position (Controlled by CustomPointer, but you can replace CustomPointer.pointerPosition with Input.MousePosition and it should work)
		
		Vector3 direction = target.transform.position - transform.position;
		transform.rotation = Quaternion.Euler (new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, (Mathf.Atan2 (direction.y,direction.x) * Mathf.Rad2Deg) - 90f, turret_rotation_speed * Time.deltaTime)));


		
	
	}
    public void Fire()
    {
        GameObject bullet = (GameObject)Instantiate(weapon_prefab, barrel_hardpoints[barrel_index].transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shot_speed);
        bullet.GetComponent<Projectile>().firing_ship = transform.parent.gameObject;
        barrel_index++; //This will cycle sequentially through the barrels in the barrel_hardpoints array

        if (barrel_index >= barrel_hardpoints.Length)
            barrel_index = 0;
    }
}
