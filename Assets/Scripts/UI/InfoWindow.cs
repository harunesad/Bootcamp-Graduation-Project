using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoWindow : GenericSingleton<InfoWindow>
{
    public TextMeshProUGUI _infoText;
    private void Start()
    {
        transform.localScale = Vector3.zero;
    }
    
    public void OpenWindow()
    {
        transform.LeanScale(Vector2.one, 0.5f).setEaseOutExpo();
    }
    
    public void CloseWindow()
    {
        transform.LeanScale(Vector2.zero, 0.5f).setEaseOutExpo();
    }
    
    public void SetInfoText(string text)
    {
        _infoText.text = text;
    }
}
