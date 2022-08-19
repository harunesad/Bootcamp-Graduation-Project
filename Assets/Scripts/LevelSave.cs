using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSave : GenericSingleton<LevelSave>
{
    public TextMeshProUGUI levelText;
    public int levelID;
    public string levelKey = "Level";
    void Start()
    {
        if (!PlayerPrefs.HasKey(levelKey))
        {
            levelID = 1;
            levelText.text = levelKey + " " + levelID;
        }
        else
        {
            levelID = PlayerPrefs.GetInt(levelKey);
            levelText.text = levelKey + " " + levelID;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
