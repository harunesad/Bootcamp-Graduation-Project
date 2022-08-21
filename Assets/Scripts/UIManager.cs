using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GenericSingleton<UIManager>
{
    public Transform _victoryTemplate;
    public Transform _defeatTemplate;
    public CanvasGroup _background;
    [SerializeField]private Transform _shopMeleeCharacterTemplate;
    [SerializeField]private Transform _shopRangedCharacterTemplate;
    [SerializeField]private Transform _MoneyTemplate;
    [SerializeField]private Transform _StartGameButton;
    [SerializeField] private TextMeshProUGUI _earnMoneyText;
    [SerializeField] private Transform _charactersInfoTemplate;
    private List<GameObject> soldiers = new List<GameObject>();
    void Start()
    {
        SetSoldierPriceUI();
    }

    private void Update()
    {
        if(!GameManager.Instance.isStarted)
            isSoldierBought();
    }

    public void SetSoldierPriceUI()
    {
        _shopMeleeCharacterTemplate.GetChild(0).Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(CostManager.Instance.GetMeleeCost().ToString());
        _shopRangedCharacterTemplate.GetChild(0).Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(CostManager.Instance.GetRangedCost().ToString());
        PlayerPrefs.SetInt("Cost", CostManager.Instance.Cost);
        _MoneyTemplate.Find("CostText").GetComponent<TextMeshProUGUI>().SetText(CostManager.Instance.Cost.ToString());
    }
    
    public void SetUIGameStarted(bool setUI)
    {
        _shopMeleeCharacterTemplate.gameObject.SetActive(setUI);
        _shopRangedCharacterTemplate.gameObject.SetActive(setUI);
        _StartGameButton.gameObject.SetActive(setUI);
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

    public void SetUILevelVictory(bool setUI)
    {
        _victoryTemplate.gameObject.SetActive(setUI);
        _background.gameObject.SetActive(setUI);
        _earnMoneyText.text = "+" + " " + "$" + PrefabManager.Instance.LevelDatas[GameManager.Instance.levelIndex].levelCoinCount;
    }
    
    public void SetUILevelFail(bool setUI)
    {
        _defeatTemplate.gameObject.SetActive(setUI);
        _background.gameObject.SetActive(setUI);
    }

    public void ShowCharactersInfoUI()
    {
        _background.gameObject.SetActive(true);
        _background.alpha = 0;
        _background.LeanAlpha(0, 1f,0.8f);
        _charactersInfoTemplate.gameObject.SetActive(true);
        _charactersInfoTemplate.localPosition = new Vector2(0, -Screen.height);
        _charactersInfoTemplate.LeanMoveLocalY(0, 1f).setEaseOutExpo();
    }
    
    public void HideCharactersInfoUI()
    {
        _charactersInfoTemplate.LeanMoveLocalY(-Screen.height, 1f).setEaseOutExpo();
        _background.LeanAlpha(1, 0f,0.8f).setEaseOutExpo().setOnComplete(() => { _background.gameObject.SetActive(false); });
    }
}
