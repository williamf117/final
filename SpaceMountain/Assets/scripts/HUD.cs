using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    public Character Player;
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
        rb2d = Player.GetComponent<Rigidbody2D>();
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
        if (target != null)
        {
            Vector3 v = new Vector3();
            v = target.transform.position - Player.transform.position;
            Debug.Log(v.magnitude);
            if(v.magnitude < 60)
            {
                Player.transform.position = Player.transform.position + v;
            }
            v = v.normalized * Time.deltaTime * 20000;
            rb2d.velocity = new Vector2(v.x, v.y);
         //   Debug.Log(rb2d.velocity);

        }	
	}
    public void TravelTo(GameObject s)
    {
        target = s;
        foreach (Transform child in hud.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    void CreateMenue()
    {
        var satelights=GameObject.FindGameObjectsWithTag("Satellite");
        Vector2 startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(startpos);
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
