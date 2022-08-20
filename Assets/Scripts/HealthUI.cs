using System.Collections;
using System.Collections.Generic;
using StatePattern;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Player Player;
    public Enemy Enemy;
    private Slider _slider;

    private void Start()
    {
        _slider = gameObject.GetComponent<Slider>();
    }

    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back,
            Camera.main.transform.rotation * Vector3.down);
        
        SetHealthUI();
    }

    void SetHealthUI()
    {
        if (Player != null)
            _slider.value = Player.health;
        else if (Enemy != null)
            _slider.value = Enemy.health;
    }
}
