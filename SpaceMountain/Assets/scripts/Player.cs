using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    GameObject Desto=null;
    [SerializeField]
    float mapMoveSpeed = 50;
    public float MapMoveSpeed
    {
        get { return mapMoveSpeed; }
        set { mapMoveSpeed = value; }
    }
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		

	}
    
    private void FixedUpdate()
    {
        if (Desto != null)
        {
          transform.position=  Vector3.MoveTowards(transform.position, Desto.transform.position, mapMoveSpeed*Time.deltaTime);


            Vector3 vectorToTarget = Desto.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * mapMoveSpeed);
        }
    }

    public void setDesto(GameObject target)
    {
        Desto = target;
    }
}
