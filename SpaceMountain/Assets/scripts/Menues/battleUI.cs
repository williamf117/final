using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class battleUI : MonoBehaviour
{
    List<GameObject> playerships = new List<GameObject>();
    [SerializeField]
    GameObject button;
    GameObject hud;
    bool initalised = false;
    List<GameObject> buttons = new List<GameObject>();
   
/// <summary>
/// a method to initalise the function called after battle controler populates the list of ships. 
/// </summary>
    public void initalize()
    {
        if (initalised == false)
        {
            hud = gameObject;
            //create the ship buttons 
            playerships = Camera.main.GetComponent<BattleControler>().Playerships;
            Vector3 pos =new Vector3(-350,-175,0);
            foreach (GameObject go in playerships)
            {
                //create the button and set the sprite on it 
                GameObject goButton = (GameObject)Instantiate(button);
                goButton.transform.position = pos;
                goButton.transform.SetParent(hud.transform, false);
                buttons.Add(goButton);
                goButton.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = go.GetComponent<SpriteRenderer>().sprite;
                goButton.GetComponent<Button>().onClick.AddListener(delegate { setSelectedShip(go); });
                pos.x += 85;
                //set the slider max to eqaqual the ship health 
                goButton.transform.GetChild(1).gameObject.GetComponent<Slider>().maxValue = go.GetComponent<Ship>().MaxHealth;
            }
            initalised = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < playerships.Count; i++)
        { if (playerships[i] == null)
            {
                playerships.Remove(playerships[i]);
                GameObject b =buttons[i] ;
                buttons.Remove(buttons[i]);
                Destroy(b);
                return;
            }
            Ship s = playerships[i].GetComponent<Ship>();
            buttons[i].transform.GetChild(1).gameObject.GetComponent<Slider>().value =s.Health;
           // Debug.Log(s.Health / s.MaxHealth);
           
        }
    }

    public void setSelectedShip(GameObject ship)
    {

        AudioManager.Instance.Play(AudioClipName.Button);
        Camera.main.GetComponent<BattleControler>().SetActive=ship.GetComponent<PlayerShip>();
    }
}
