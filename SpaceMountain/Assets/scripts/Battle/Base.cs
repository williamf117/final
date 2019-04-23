using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Base : MonoBehaviour
{
    float onlineTimer = 0;
    const float maxonlineTime = 120;
    float health = 100;
    float rainge = 20;
    Slider buildslider;
    float cooldowntime=1;
    float maxcooldown = 1;
    bool oncooldown = false;
    GameObject[] Eanamys;
    List<PlayerControlledTurret> turrets = new List<PlayerControlledTurret>();
    // Start is called before the first frame update
    void Start()
    {
        //find all  the turrets 
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<PlayerControlledTurret>() != null)
            {
                turrets.Add(child.gameObject.GetComponent<PlayerControlledTurret>());
            }
            //find the slider for build time
            if (child.gameObject.GetComponent<Canvas>() != null)
            {
                buildslider= child.transform.GetChild(0).GetComponent<Slider>();
               // buildslider = child.gameObject.GetComponent<Slider>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //update list of eanamy ships
        Eanamys = GameObject.FindGameObjectsWithTag("HostileFleet");
        GameObject closest = null;
        float closeD = Mathf.Infinity;
        //find the closest and shoot it if in rainge 
        foreach(GameObject go in Eanamys)
        {
            float distence =Mathf.Abs( (transform.position - go.transform.position).magnitude);
            if (distence < closeD)
            {
                closest = go;
                closeD = distence;
            }

        }
        //target the closest 
        foreach(PlayerControlledTurret t in turrets) {
            t.Target(closest);
        }

        
        if (closeD < rainge)
        { if (!oncooldown)
            {
                foreach (PlayerControlledTurret T in turrets)
                {
                    
                    T.Fire();  
               }
                oncooldown = true;

            }
            else
            {
                cooldowntime -= Time.deltaTime;
                if (cooldowntime < 0)
                {
                    cooldowntime = maxcooldown;
                    oncooldown = false;
                }
            }

        }

        //timeing for the bar 
        onlineTimer += Time.deltaTime;
        buildslider.value = onlineTimer / maxonlineTime;

        if (onlineTimer > maxonlineTime)
        {
            rainge = 200;
            maxcooldown = .5f;
        }
    }
}
