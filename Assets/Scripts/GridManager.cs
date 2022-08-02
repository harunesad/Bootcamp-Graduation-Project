using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : GenericSingleton<GridManager>
{
    public Transform GridCellPrefab;
    public Transform Cube;
    public Transform PrefabObject;
    public Node[,] _nodes;

    [SerializeField] private int _height;
    [SerializeField] private int _width;

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
        _nodes = new Node[_width, _height];
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var cell = Instantiate(GridCellPrefab, new Vector3(i, 0, j), Quaternion.identity);
                cell.name = "Cell " + i + " " + j;
                cell.transform.parent = transform;
                _nodes[i, j] = new Node(true, new Vector3(i, 0, j), cell.transform);
            }
        }
    }

    Node FindEmptyCell()
    {
        //6'ya 6 gridin yarısına yerleştirebilmesi için böyle bir for döngüsü yazdım.
        for (int i = _width - 1; i >= 0; i--)
        {
            for (int j = _height - 1; j >= 3; j--)
            {
                if (_nodes[i, j].IsPlaceable)
                {
                    return _nodes[i, j];
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
            }
            PrefabObject = null;
        }
    }

    public void CreatePrefabObejct()
    {
        var node = FindEmptyCell();
        if (PrefabObject == null && node != null)
        {
            PrefabObject = Instantiate(Cube, transform.position, Quaternion.identity);
        }
    }
}
