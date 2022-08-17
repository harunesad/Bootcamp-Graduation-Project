using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField]private Transform _shopMeleeCharacterTemplate;
    [SerializeField]private Transform _shopRangedCharacterTemplate;
    [SerializeField]private Transform _MoneyTemplate;
    [SerializeField]private Transform _StartGameButton;
    void Start()
    {
        SetSoldierPriceUI();
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
}
