using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{
    public Image settingPopup;
    public void GameSceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SettingMenuOpen()
    {
        settingPopup.gameObject.SetActive(true);
    }
    public void SettingMenuClose()
    {
        settingPopup.gameObject.SetActive(false);
    }
}
