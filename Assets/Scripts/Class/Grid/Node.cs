using System;
using UnityEngine;

[Serializable]
public class Node
{
    public bool IsPlaceable;
    public Vector3 CellPosition;
    public string Tag;

    public Node(bool isPlaceable, Vector3 cellPosition, string tag)
    {
        IsPlaceable = isPlaceable;
        CellPosition = cellPosition;
        Tag = tag;
    }
}
