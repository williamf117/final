using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.Events;
public class Fleet : MonoBehaviour
{
    GameObject Desto=null;
    GameObject go;
    [SerializeField]
    List<GameObject> ships = new List<GameObject>();

    List<GameObject> fleet = new List<GameObject>();
    BattleStartEvent bs;
   
    
    public GameObject Target
    {
        get { return Desto; }
        set { Desto = value; }
    }
    float mapMoveSpeed =10;


    // Start is called before the first frame update
    void Start()
    {
        bs = new BattleStartEvent();
        EventManager.AddEnterBattleInvoker(this);
        //fleet.Add(ships[0]);
        //fleet.Add(ships[0]);
        //fleet.Add(ships[0]);
        //fleet.Add(ships[0]);
       
    }
    public void MakeFleet(int danger)
    {
        fleet.Clear();
        for(int i = 0; i<danger; i++)
        {
            fleet.Add(ships[Random.Range(0,ships.Count)]);
        }
    }

   /// <summary>
   /// for moving the fleet 
   /// </summary>
    private void FixedUpdate()
    { 
        
         if (Desto != null)
        {
          transform.position=  Vector3.MoveTowards(transform.position, Desto.transform.position, mapMoveSpeed*Time.deltaTime);


            Vector3 vectorToTarget = Desto.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * mapMoveSpeed);
            mapMoveSpeed += 20 * Time.deltaTime;
            
        
        }
        else
        {
            mapMoveSpeed = 10;
        }
    }

    
     void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("coll");
        if (collision.gameObject.tag == "Player")
        {

            bs.Invoke(fleet);
            //Destroy(gameObject);
        }
    }



    public void AddEnterBattleListener(UnityAction<List<GameObject>> listener)
    {
        bs.AddListener(listener); 
    }
   
}
