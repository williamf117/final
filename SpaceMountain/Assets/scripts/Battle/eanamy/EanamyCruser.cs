using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EanamyCruser : EanamyBase
{
    List<PlayerControlledTurret> turrets = new List<PlayerControlledTurret>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<PlayerControlledTurret>() != null)
            {
                turrets.Add(child.gameObject.GetComponent<PlayerControlledTurret>());
            }
        }

    }

  
  protected override  void fireOnTarget()
    {

        //turn to face target
        rb2d.velocity = Vector2.zero;


        //fire if you can
        if (!oncooldown)
        {
            foreach (PlayerControlledTurret t in turrets)
            {
                t.Fire();
            }
            oncooldown = true;
            


        }
        //don't fire while on cooldown
        if (oncooldown)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                oncooldown = false;
                cooldown = 3;
            }
        }

    }

}
