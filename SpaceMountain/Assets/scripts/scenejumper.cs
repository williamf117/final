using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scenejumper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindObjectOfType<GameManager>());
    }

    // Update is called once per frame
    public void loadscene()
    {
        SceneManager.LoadScene("Jupiter");
    }
    public void quit() {
        Application.Quit();
    }
    public void main()
    {
        SceneManager.LoadScene("Menue");
    }
    public void controles()
    {
        SceneManager.LoadScene("Controles");
    }
}
