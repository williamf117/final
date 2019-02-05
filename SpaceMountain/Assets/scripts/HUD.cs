using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    [SerializeField]
    Player player;
    Rigidbody2D rb2d;
    GameObject target=null;
    [SerializeField]
    GameObject BlankButton;
    [SerializeField]
    Canvas hud;
    bool clicklastfraim = false;
    List<Button> buttons;
    // Use this for initialization
    void Start () {
       
        hud = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () { 
        if(Input.GetMouseButtonDown(1)&&!clicklastfraim)
        {
            clicklastfraim = true;
            CreateMenue();
        }
        else if (!Input.GetMouseButtonDown(1) && clicklastfraim)
        {
            clicklastfraim = false;
        }
       
	}
    public void TravelTo(GameObject s)
    {
        player.setDesto(s);//set the players desto
        foreach (Transform child in hud.transform)
        {
            GameObject.Destroy(child.gameObject);//destroy the buttons 
        }
    }
    void CreateMenue()
    {
        var satelights=GameObject.FindGameObjectsWithTag("Satellite");
        Vector2 startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(startpos);
        int i = 1;
        foreach(GameObject go in satelights)
        {
            GameObject goButton = (GameObject)Instantiate(BlankButton);
            goButton.transform.SetParent(hud.transform, false);
            // goButton.transform.localScale = new Vector3(1, 1, 1);
            Vector3 pos = goButton.transform.position;
            pos.y -= 20 * i;
            goButton.transform.position = pos;

            Button tempButton = goButton.GetComponent<Button>();
            tempButton.onClick.AddListener(delegate { TravelTo(go); });
            var text= tempButton.GetComponentInChildren<Text>().text = go.name;
            i++;


        }
    }
}
