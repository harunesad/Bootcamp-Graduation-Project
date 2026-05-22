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

    public int CheckEnemyCount()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int count = 0;
        foreach (var enemyObj in enemies)
        {
            var enemy = enemyObj.GetComponent<StatePattern.Enemy>();
            if (enemy != null && !enemy.isDead && enemy.health > 0)
            {
                count++;
            }
        }
        return count;
    }
    
    public List<GameObject> CheckSoldierCount()
    {
        soldiers.Clear();
        for (var i = 1; i < 4; i++)
        {
            var soldierObjects = GameObject.FindGameObjectsWithTag("Melee " + i);
            if (soldierObjects != null)
            {
                for (int j = 0; j < soldierObjects.Length; j++)
                {
                    var p = soldierObjects[j].GetComponent<StatePattern.Player>();
                    if (p != null && !p.isDead && p.health > 0)
                    {
                        soldiers.Add(soldierObjects[j]);
                    }
                }
            }
        }
        for (var i = 1; i < 4; i++)
        {
            var soldierObjects = GameObject.FindGameObjectsWithTag("Archer " + i);
            if(soldierObjects != null)
                for (int j = 0; j < soldierObjects.Length; j++)
                {
                    var p = soldierObjects[j].GetComponent<StatePattern.Player>();
                    if (p != null && !p.isDead && p.health > 0)
                    {
                        soldiers.Add(soldierObjects[j]);
                    }
                }
        }

        return soldiers;
    }

    public void NextLevel()
    {
        if (levelIndex + 1 >= PrefabManager.Instance.LevelDatas.Count)
        {
            Debug.Log("Tüm seviyeler tamamlandı! Tebrikler!");
            return;
        }

        JsonController.Instance.JsonSave();
        levelIndex++;
        GridManager.Instance.NextLevel();
        UIManager.Instance.SetUIGameStarted(true);
        CostManager.Instance.AddCost(PrefabManager.Instance.LevelDatas[levelIndex - 1].levelCoinCount);
        SetPlayerPrefs();
        UIManager.Instance.SetSoldierPriceUI();

        LevelSave.Instance.levelID++;
        PlayerPrefs.SetInt(LevelSave.Instance.levelKey, LevelSave.Instance.levelID);
        LevelSave.Instance.levelID = PlayerPrefs.GetInt(LevelSave.Instance.levelKey);
        LevelSave.Instance.levelText.text = LevelSave.Instance.levelKey + " " + LevelSave.Instance.levelID;

        PlayerPrefs.SetInt(levelIndexKey, levelIndex);
        levelIndex = PlayerPrefs.GetInt(levelIndexKey);
    }

    private static void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("Cost", CostManager.Instance.Cost);
        PlayerPrefs.SetInt("MeleePrice", CostManager.Instance.MeleePrice);
        PlayerPrefs.SetInt("RangedPrice", CostManager.Instance.RangedPrice);
        PlayerPrefs.SetFloat("MeleeFactor", CostManager.Instance.MeleeFactor);
        PlayerPrefs.SetFloat("RangedFactor", CostManager.Instance.RangedFactor);
    }

    public void RestartLevel()
    {
        CostManager.Instance.ResetCostManager();
        GridManager.Instance.RestartLevel();
        UIManager.Instance.SetUIGameStarted(true);
        UIManager.Instance.SetSoldierPriceUI();
    }
}
