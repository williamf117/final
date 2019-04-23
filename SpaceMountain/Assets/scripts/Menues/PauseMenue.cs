using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenue : MonoBehaviour
{
    public void quit()
    {
        SceneManager.LoadScene("Menue");
        Time.timeScale = 1;
    }
    public void Resuem()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
