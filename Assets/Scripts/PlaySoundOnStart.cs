using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    
    void Start()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "SampleScene")
        {
            SoundManager.Instance.PlayMainSound(_audioClip);
        }
        else
        {
            SoundManager.Instance.PlayBattleSound(_audioClip);
        }
    }
}
