using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridPosition
{
    public int PosX;
    public int PosZ;
    public string PrefabName;

    public PlayerGridPosition()
    {
    }

    public PlayerGridPosition(int x, int z, string prefabName)
    {
        PosX = x;
        PosZ = z;
        PrefabName = prefabName;
    }
}
