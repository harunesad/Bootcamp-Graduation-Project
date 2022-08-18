using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : GenericSingleton<Ready>
{
    //public static Ready instance;
    public bool isReady = true;

    //private void Awake()
    //{
    //    instance = this;
    //}
    public void IsReady()
    {
        isReady = false;
    }
}
