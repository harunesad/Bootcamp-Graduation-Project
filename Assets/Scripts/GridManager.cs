using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : GenericSingleton<GridManager>
{
    public Transform GridCellPrefab;
    public Transform Melee_1;
    public Transform Range_1;
    public Transform PrefabObject;
    public Node[,] Nodes;
    public int Height;
    public int Width;

    void Start()
    {
        CreateGrid();
    }

    void Update()
    {
        PutPrefabOnGrid();
    }

    void CreateGrid()
    {
        Nodes = new Node[Width, Height];
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                var cell = Instantiate(GridCellPrefab, new Vector3(i, 0, j), Quaternion.identity);
                cell.name = "Cell " + i + " " + j;
                cell.transform.parent = transform;
                Nodes[i, j] = new Node(true, new Vector3(i, 0, j), cell.transform, "");
            }
        }
    }

    Node FindEmptyCell()
    {
        //6'ya 6 gridin yarısına yerleştirebilmesi için böyle bir for döngüsü yazdım.
        for (var i = Width - 1; i >= 0; i--)
        {
            for (var j = Height - 1; j >= Height / 2; j--)
            {
                if (Nodes[i, j].IsPlaceable)
                {
                    return Nodes[i, j];
                }
            }
        }
        return null;
    }

    void PutPrefabOnGrid()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var node = FindEmptyCell();
            if (PrefabObject != null && node != null)
            {
                PrefabObject.transform.position = node.CellPosition + new Vector3(0, 0.25f, 0);
                node.IsPlaceable = false;
                node.Tag = PrefabObject.tag;
            }
            PrefabObject = null;
        }
    }

    public void CreateMeleeSoldier()
    {
        var node = FindEmptyCell();
        if (PrefabObject == null && node != null)
        {
            PrefabObject = Instantiate(Melee_1, transform.position, Quaternion.identity);
        }
    }
    
    public void CreateRangeSoldier()
    {
        var node = FindEmptyCell();
        if (PrefabObject == null && node != null)
        {
            PrefabObject = Instantiate(Range_1, transform.position, Quaternion.identity);
        }
    }
}
