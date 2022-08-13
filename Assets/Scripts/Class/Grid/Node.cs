using System;
using UnityEngine;

[Serializable]
public class Node
{
    public bool IsPlaceable;
    public Vector3 CellPosition;
    public Transform Object;
    public string Tag;

    public Node(bool isPlaceable, Vector3 cellPosition, Transform objectTransform, string tag)
    {
        IsPlaceable = isPlaceable;
        CellPosition = cellPosition;
        Object = objectTransform;
        Tag = tag;
    }
}
