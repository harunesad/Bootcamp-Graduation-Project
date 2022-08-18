using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : GenericSingleton<GameManager>
{
    public bool isStarted = false;
    public int levelIndex = 1;

    private List<GameObject> soldiers = new List<GameObject>();
    public void Started()
    {
        isStarted = true;
        UIManager.Instance.SetUIGameStarted();
    }

    private void Update()
    {
        if (isStarted)
        {
            if (isAllEnemiesDead())
                ShowLevelVictoryUI();
            if (isAllSoldiersDead())
                ShowLevelFailUI();
        }
    }

    void ShowLevelVictoryUI()
    {
        UIManager.Instance.SetUILevelVictory(true);
    }
    
    void ShowLevelFailUI()
    {
        UIManager.Instance.SetUILevelFail(true);
    }

    bool isAllEnemiesDead()
    {
        var enemiesCount = CheckEnemyCount();
        if(enemiesCount > 0)
            return false;
        return true;
            
    }

    bool isAllSoldiersDead()
    {
        var soldiers = CheckSoldierCount();
        if (soldiers.Count > 0)
            return false;
        return true;
    }

    int CheckEnemyCount()
    {
        var count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        return count;
    }
    
    List<GameObject> CheckSoldierCount()
    {
        soldiers.Clear();
        for (var i = 1; i < 4; i++)
        {
            var soldier = GameObject.FindWithTag("Melee " + i);
            if(soldier != null)
                soldiers.Add(soldier);
        }
        for (var i = 1; i < 4; i++)
        {
            var soldier = GameObject.FindWithTag("Archer " + i);
            if(soldier != null)
                soldiers.Add(soldier);
        }

        return soldiers;
    }
}
