using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
   Vector3 target=Vector3.zero;
    float rotationspeed = 3;
    bool mouseon;

    public bool MouseOn
    {
        get { return mouseon; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != Vector3.zero)
        {
            //turn to face target

            Vector3 vectorToTarget = target - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationspeed);

            //move to postion 
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.x,target.y,-1), .01f);
            
        }
    }
    public void MoveToPosition(Vector3 position)
    {
        target = position;
       
        Debug.Log("CALLED");
    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        mouseon = true;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        mouseon = false;
    }
}
