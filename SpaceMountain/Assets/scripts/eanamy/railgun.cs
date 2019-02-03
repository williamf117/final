using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class railgun : MonoBehaviour
{
    [SerializeField]
    float maxrainge = 15, minrainge = 5;//effective rainge of the gun. 
    bool targetInrainge = false;
    GameObject[] playerships;
    GameObject currenttarget; 
    float rotationspeed=3;
    bool inrainge=false;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currenttarget == null)
        {
            currenttarget = FindTarget();
        }
        float rainge = Vector2.Distance(transform.position, currenttarget.transform.position);
       ;
        if (rainge > minrainge && rainge < maxrainge)
        {
            inrainge = true;
        }
        else
        {
            inrainge = false;
        } Debug.Log(inrainge );


        //rotate to face the target if inrainge
        if (inrainge)
        {
            //turn to face target
            Vector3 vectorToTarget = currenttarget.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);
        }
    }

    GameObject FindTarget()
    {
        playerships = GameObject.FindGameObjectsWithTag("PlayerShip");
        int target= Random.Range(0, playerships.Length - 1);
        Debug.Log(playerships[target]);
        return playerships[target];
        
    }
}
