using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : GenericSingleton<StartGame>
{
    public bool isStarted = false;
    public void Started()
    {
        isStarted = true;
        UIManager.Instance.SetUIGameStarted();
    }
}
