using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : GenericSingleton<GameManager>
{
    public bool isStarted = false;
    public int levelIndex = 0;
    public string levelIndexKey = "LevelIndex";
    public List<GameObject> soldiers = new List<GameObject>();
    private void Start()
    {
        if (!PlayerPrefs.HasKey(levelIndexKey))
        {
            levelIndex = 0;
        }
        else
        {
            levelIndex = PlayerPrefs.GetInt(levelIndexKey);
        }
    }
    public void Started()
    {
        isStarted = true;
        UIManager.Instance.SetUIGameStarted(false);
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

    public bool isAllEnemiesDead()
    {
        var enemiesCount = CheckEnemyCount();
        if(enemiesCount > 0)
            return false;
        return true;
            
    }

    public bool isAllSoldiersDead()
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
    
    public List<GameObject> CheckSoldierCount()
    {
        soldiers.Clear();
        for (var i = 1; i < 4; i++)
        {
            var soldier = GameObject.FindGameObjectsWithTag("Melee " + i);
            if (soldier != null)
            {
                for (int j = 0; j < soldier.Length; j++)
                {
                    soldiers.Add(soldier[j]);
                }
            }
        }
        for (var i = 1; i < 4; i++)
        {
            var soldier = GameObject.FindGameObjectsWithTag("Archer " + i);
            if(soldier != null)
                for (int j = 0; j < soldier.Length; j++)
                {
                    soldiers.Add(soldier[j]);
                }
        }

        return soldiers;
    }

    public void NextLevel()
    {
        levelIndex++;
        GridManager.Instance.NextLevel();
        UIManager.Instance.SetUIGameStarted(true);
        CostManager.Instance.AddCost(PrefabManager.Instance.LevelDatas[levelIndex - 1].levelCoinCount);
        UIManager.Instance.SetSoldierPriceUI();

        LevelSave.Instance.levelID++;
        PlayerPrefs.SetInt(LevelSave.Instance.levelKey, LevelSave.Instance.levelID);
        LevelSave.Instance.levelID = PlayerPrefs.GetInt(LevelSave.Instance.levelKey);
        LevelSave.Instance.levelText.text = LevelSave.Instance.levelKey + " " + LevelSave.Instance.levelID;

        PlayerPrefs.SetInt(levelIndexKey, levelIndex);
        levelIndex = PlayerPrefs.GetInt(levelIndexKey);
    }
    
    public void RestartLevel()
    {
        CostManager.Instance.ResetCostManager(levelIndex);
        GridManager.Instance.RestartLevel();
        UIManager.Instance.SetUIGameStarted(true);
        UIManager.Instance.SetSoldierPriceUI();
    }
}
