using UnityEngine;

public class Node
{
    public bool IsPlaceable;
    public Vector3 CellPosition;
    public Transform Object;

    public Node(bool isPlaceable, Vector3 cellPosition, Transform objectTransform)
    {
        IsPlaceable = isPlaceable;
        CellPosition = cellPosition;
        Object = objectTransform;
    }
}
