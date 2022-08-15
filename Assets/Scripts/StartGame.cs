using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static StartGame instance;
    public bool isStarted = false;
    private void Awake()
    {
        instance = this;
    }
    public void Started()
    {
        isStarted = true;
    }
}
