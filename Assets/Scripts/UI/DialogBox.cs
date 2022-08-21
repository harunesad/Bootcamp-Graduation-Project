using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;

    private void OnEnable()
    {
        background.gameObject.SetActive(true);
        background.alpha = 0;
        background.LeanAlpha(0, 1f,0.8f);

        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 1f).setEaseOutExpo().delay = 0.1f;
    }
    
    public void Close()
    {
        background.LeanAlpha(1, 0, 0.8f).setEaseInExpo();
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(onComplete);
    }
    
    void onComplete()
    {
        gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }
}
