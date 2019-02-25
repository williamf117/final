using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
public class Fleet : MonoBehaviour
{
    GameObject Desto=null;
    GameObject go;
    [SerializeField]
    List<GameObject> ships = new List<GameObject>();
    
    public GameObject Target
    {
        get { return Desto; }
        set { Desto = value; }
    }
    float mapMoveSpeed =10;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
