using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : GenericSingleton<CostManager>
{
    public int Cost;
    public int MeleePrice;
    public int RangedPrice;
    public float MeleeFactor = 1f;
    public float RangedFactor = 1f;
    // override public void Awake()
    // {
    //     base.Awake();
    //     if (!PlayerPrefs.HasKey("Cost"))
    //     {
    //         Cost = 10000;
    //         PlayerPrefs.SetInt("Cost", Cost);
    //     }
    //     else
    //     {
    //         Cost = PlayerPrefs.GetInt("Cost");
    //     }
    // }
    public enum SoldierType
    {
        None,
        Melee,
        Ranged
    }
    
    public int GetSoldierCost(SoldierType type)
    {   
        switch (type)
        {
            case SoldierType.Melee:
                return Convert.ToInt32(MeleePrice);
            case SoldierType.Ranged:
                return Convert.ToInt32(RangedPrice);
            case SoldierType.None:
                return 0;
            default:
                return 0;
        }
    }

    public void IncreaseMeleeFactor()
    {
        MeleeFactor += 0.06f;
    }
    
    public void IncreaseRangedFactor()
    {
        RangedFactor += 0.05f;
    }
    
    public void AddCost(int cost)
    {
        Cost += cost;
    }
    
    public void SubCost(int cost)
    {
        Cost -= cost;
    }

    public int GetMeleeCost()
    {
        return Convert.ToInt32(MeleePrice);
    }
    
    public void SetMeleeCost(int cost)
    {
        MeleePrice = cost;
        UIManager.Instance.SetSoldierPriceUI();
    }
    
    public int GetRangedCost()
    {
        return Convert.ToInt32(RangedPrice);
    }

    public void SetRangedCost(int cost)
    {
        RangedPrice = cost;
        UIManager.Instance.SetSoldierPriceUI();
        
    }
    
    public void BuyMeleeSoldier(int cost)
    {
        SubCost(cost);
        IncreaseMeleeFactor();
        SetMeleeCost(Convert.ToInt32(MeleePrice * MeleeFactor));
    }
    
    public void BuyRangedSoldier(int cost)
    {
        SubCost(cost);
        IncreaseRangedFactor();
        SetRangedCost(Convert.ToInt32(RangedPrice * RangedFactor));
    }

    public int GetSoldierPrice(SoldierType soldierType)
    {
        var cost = GetSoldierCost(soldierType);
        return cost;
    }

    public bool CheckMoneyAmount(int cost)
    {
        if (cost > Cost)
        {
            InfoWindow.Instance.OpenWindow();
            StartCoroutine(CloseInfoWindow());
            return false;
        }
        return true;
    }
    
    IEnumerator CloseInfoWindow()
    {
        yield return new WaitForSeconds(1.5f);
        InfoWindow.Instance.CloseWindow();
    }

    public void ResetCostManager(int levelIndex)
    {
        switch (levelIndex)
        {
            case 0:
                Cost = 600;
                MeleePrice = 100;
                RangedPrice = 155;
                MeleeFactor = 1f;
                RangedFactor = 1f;
                break;
            case 1:
                Cost = 600 + PrefabManager.Instance.LevelDatas[levelIndex].levelCoinCount;
                MeleePrice = 150;
                RangedPrice = 250;
                MeleeFactor = 1.3f;
                RangedFactor = 1.2f;
                break;
            case 2:
                Cost = 600 + PrefabManager.Instance.LevelDatas[levelIndex].levelCoinCount;
                MeleePrice = 350;
                RangedPrice = 550;
                MeleeFactor = 1.6f;
                RangedFactor = 1.5f;
                break;
        }
    }
}
