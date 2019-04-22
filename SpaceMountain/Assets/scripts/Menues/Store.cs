using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    List<GameObject> ships = new List<GameObject>();
    // Start is called before the first frame update

    public void BuyShip(GameObject ship)
    {

       float price = ship.GetComponent<PlayerShip>().Price;
        if (price <= GameManager.instance.Funds)
        {
            AudioManager.Instance.Play(AudioClipName.Button);
            GameManager.instance.playerfleet.Add(ship);
            GameManager.instance.Funds -= price;
            Close();
        }
        else
        {
            AudioManager.Instance.Play(AudioClipName.ButtonFail);
        }
    }

    public void Close()
    {
        AudioManager.Instance.Play(AudioClipName.Button);
        foreach (Transform child in transform)
        {
            Destroy(transform.gameObject);
        }
        Destroy(gameObject);
    }

}
