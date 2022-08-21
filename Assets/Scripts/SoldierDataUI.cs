using System.Collections;
using System.Collections.Generic;
using StatePattern;
using TMPro;
using UnityEngine;

public class SoldierDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _armorText;
    void Start()
    {
        var soldier = PrefabManager.Instance.GetPrefab(gameObject.name);
        _attackText.text = soldier.GetComponent<Player>().attack.ToString();
        _armorText.text = soldier.GetComponent<Player>().armor.ToString();
    }
}
