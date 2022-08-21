using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{
    public Transform settingPopup;
    public Transform contributedsPopup;
    public Transform door;
    public CanvasGroup background;
    public void GameSceneLoad()
    {
        SoundManager.Instance.PlayOpenGateSound(SoundManager.Instance.transform.GetChild(2).GetComponent<AudioSource>().clip);
        background.gameObject.SetActive(true);
        background.alpha = 0;
        background.LeanAlpha(0, 1f,4f);
        door.LeanScaleZ(-18.76f, 4f).setEaseInExpo().setOnComplete(() =>
        {
            StartCoroutine(waiter());
            SoundManager.Instance._openGateAudioSource.Stop();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SoundManager.Instance.OnLevelWasLoaded(1);
        });
        
    }
    
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
    }
    
    public void SettingMenuOpen()
    {
        settingPopup.gameObject.SetActive(true);
        settingPopup.localPosition = new Vector2(0, -Screen.height);
        settingPopup.LeanMoveLocalY(0, 1f).setEaseOutExpo();
    }
    public void SettingMenuClose()
    {
        settingPopup.LeanMoveLocalY(-Screen.height, 1f).setEaseInExpo().setOnComplete(() => { settingPopup.gameObject.SetActive(false); });
    }
    
    public void ContributedsMenuOpen()
    {
        contributedsPopup.gameObject.SetActive(true);
        contributedsPopup.localPosition = new Vector2(0, -Screen.height);
        contributedsPopup.LeanMoveLocalY(0, 1f).setEaseOutExpo();
    }
    
    public void ContributedsMenuClose()
    {
        contributedsPopup.LeanMoveLocalY(-Screen.height, 1f).setEaseInExpo().setOnComplete(() => { contributedsPopup.gameObject.SetActive(false); });
    }
}
