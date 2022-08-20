using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonController : GenericSingleton<JsonController>
{
    public void JsonSave()
    {
        var soldiers = GridManager.Instance.GetSoldiersPlayerGridPositions();
        string jsonString = JsonConvert.SerializeObject(soldiers);
        File.WriteAllText(Application.dataPath + "/Resources/JsonData/Soldiers.json", jsonString);
    }
    
    public List<PlayerGridPosition> JsonLoad()
    {
        var path = Application.dataPath + "/Resources/JsonData/Soldiers.json";
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<PlayerGridPosition>>(json);
    }
}
