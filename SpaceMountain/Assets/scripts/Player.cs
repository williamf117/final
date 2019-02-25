using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    int funds=10000;
    int fule = 100;
    GameObject gamemanager;
    public int Fule
    {
        get { return fule; }
    }
    public int Funds
    {
        get { return funds; }
    }

    GameObject Desto=null;
    [SerializeField]
    float mapMoveSpeed = 10;
    public float MapMoveSpeed
    {
        get { return mapMoveSpeed; }
        set { mapMoveSpeed = value; }
    }
	// Use this for initialization
	void Start () {
        Sprite s = GetComponent<SpriteRenderer>().sprite;

        gamemanager = GameObject.FindGameObjectWithTag("GameController");
        //make a larger versoion of the sprite for the map veiw 
        GameObject go = new GameObject();
        go.AddComponent<SpriteRenderer>().sprite = s;
        go.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        go.transform.localScale = new Vector3(10, 10, -1);
        //  Instantiate(go);
        go.transform.parent = gameObject.transform;
        go.layer = 9;
    }
	
	// Update is called once per frame
	void Update () {

        if (gamemanager.GetComponent<MissionBase>().completed)
        {
            funds += gamemanager.GetComponent<MissionBase>().reward;
            gamemanager.GetComponent<MissionBase>().reward = 0;
        }
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
