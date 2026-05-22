using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SoldierDataWrapper
{
    public List<PlayerGridPosition> soldiers;
}

public class JsonController : GenericSingleton<JsonController>
{
    public void JsonSave()
    {
        var soldiersList = GridManager.Instance.GetSoldiersPlayerGridPositions();
        var wrapper = new SoldierDataWrapper { soldiers = soldiersList };
        string jsonString = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(Application.dataPath + "/Resources/JsonData/Soldiers.json", jsonString);
    }
    
    public List<PlayerGridPosition> JsonLoad()
    {
        var path = Application.dataPath + "/Resources/JsonData/Soldiers.json";
        if (!File.Exists(path))
        {
            return new List<PlayerGridPosition>();
        }
        var json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<PlayerGridPosition>();
        }
        
        try
        {
            var trimmedJson = json.Trim();
            if (trimmedJson.StartsWith("["))
            {
                trimmedJson = "{\"soldiers\":" + trimmedJson + "}";
            }
            var wrapper = JsonUtility.FromJson<SoldierDataWrapper>(trimmedJson);
            if (wrapper != null && wrapper.soldiers != null)
            {
                return wrapper.soldiers;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("JSON Load Error: " + ex.Message);
        }
        
        return new List<PlayerGridPosition>();
    }
}
