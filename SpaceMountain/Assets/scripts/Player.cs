using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
 
    GameManager gm;
    [SerializeField]
    List<GameObject> AllShips = new List<GameObject>();
    List<GameObject> playerFleet = new List<GameObject>();
   
    public GameObject target
    {
        get { return Desto; }
    }

    public int Fule
    {
        get { return 100; }
    }
    public float Funds
    {
        get { return gm.Funds; }
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
        gm = GameManager.instance;
      
      
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
        
        
      //  gm.playerfleet = playerFleet;
	}
    
    private void FixedUpdate()
    {
        if (Desto != null)
        {
          transform.position=  Vector3.MoveTowards(transform.position, Desto.transform.position, mapMoveSpeed*Time.deltaTime);
            mapMoveSpeed += 10 * Time.deltaTime;

            Vector3 vectorToTarget = Desto.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * mapMoveSpeed);
        }
        else
        {
            mapMoveSpeed = 10;
        }
    }

    public void setDesto(GameObject target)
    {
        Desto = target;
    }

}
