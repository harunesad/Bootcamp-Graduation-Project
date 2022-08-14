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
                return Convert.ToInt32(MeleePrice * MeleeFactor);
            case SoldierType.Ranged:
                return Convert.ToInt32(RangedPrice * RangedFactor);
            case SoldierType.None:
                return 0;
            default:
                return 0;
        }
    }

    public void IncreaseMeleeFactor()
    {
        MeleeFactor += 0.05f;
    }
    
    public void IncreaseRangedFactor()
    {
        RangedFactor += 0.06f;
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
            return false;
        return true;
    }
}
