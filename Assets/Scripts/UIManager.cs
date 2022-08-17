using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField]private Transform _shopMeleeCharacterTemplate;
    [SerializeField]private Transform _shopRangedCharacterTemplate;
    [SerializeField]private Transform _MoneyTemplate;
    [SerializeField]private Transform _StartGameButton;
    private List<GameObject> soldiers = new List<GameObject>();
    void Start()
    {
        SetSoldierPriceUI();
    }

    private void Update()
    {
        if(!StartGame.Instance.isStarted)
            isSoldierBought();
    }

    public void SetSoldierPriceUI()
    {
        _shopMeleeCharacterTemplate.GetChild(0).Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(CostManager.Instance.GetMeleeCost().ToString());
        _shopRangedCharacterTemplate.GetChild(0).Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(CostManager.Instance.GetRangedCost().ToString());
        _MoneyTemplate.Find("CostText").GetComponent<TextMeshProUGUI>().SetText(CostManager.Instance.Cost.ToString());
    }
    
    public void SetUIGameStarted()
    {
        _shopMeleeCharacterTemplate.gameObject.SetActive(false);
        _shopRangedCharacterTemplate.gameObject.SetActive(false);
        _StartGameButton.gameObject.SetActive(false);
    }

    void isSoldierBought()
    {
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
        
        _StartGameButton.gameObject.SetActive(soldiers.Count != 0);
    }
}
