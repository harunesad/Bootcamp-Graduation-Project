using System.Collections;
using System.Collections.Generic;
using StatePattern;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Player meleePlayer;
    public Enemy meleeEnemy;
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
        if (meleePlayer != null)
            _slider.value = meleePlayer.health;
        else if (meleeEnemy != null)
            _slider.value = meleeEnemy.health;
    }
}
