using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class SaveGame : MonoBehaviour
{
    public static void save()
    {
        StreamWriter save = new StreamWriter("save.txt");
        save.WriteLine(SceneManager.GetActiveScene().name);
        save.WriteLine("Player ships ");
        foreach(GameObject ship in GameManager.instance.playerfleet)
        {
            save.WriteLine(ship.GetComponent<PlayerShip>().GetIndex);
        }
        save.WriteLine("mission number ");
        save.WriteLine(GameManager.instance.MissionCount);

        save.WriteLine("funds and fuel");
        save.WriteLine(GameManager.instance.Funds + " " + GameManager.instance.Fuel);


        save.Close();

    }
}
