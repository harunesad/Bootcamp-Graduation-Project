using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : GenericSingleton<PrefabManager>
{
    public GameObject Melee1;
    public GameObject Melee2;
    public GameObject Melee3;

    public GameObject Archer1;
    public GameObject Archer2;
    public GameObject Archer3;

    public GameObject GetPrefab(string prefabName)
    {
        return prefabName switch
        {
            "Melee1" => Melee1,
            "Melee2" => Melee2,
            "Melee3" => Melee3,
            "Archer1" => Archer1,
            "Archer2" => Archer2,
            "Archer3" => Archer3,
            _ => null
        };
    }
}
