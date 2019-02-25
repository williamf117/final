﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBreafing : MonoBehaviour
{
    /// <summary>
    /// called to close the misssion breafing.
    /// </summary>
    public void close()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
            
        }
        Destroy(gameObject);
    }
}
