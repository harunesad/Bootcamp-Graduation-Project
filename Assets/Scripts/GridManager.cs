using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using StatePattern;

public class GridManager : GenericSingleton<GridManager>
{
    public Transform GridCellPrefab;
    public Node[,] Nodes;
    public int Height;
    public int Width;
    public LevelData levelData;
    public ParticleSystem SpawnParticles;
    
    private GameObject PrefabObject;
    
    void Start()
    {
        CreateGrid();
        AddEnemyCells();
        CreateEnemies();
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
                Nodes[i, j] = new Node(true, new Vector3(i, 0, j), "");
            }
        }
    }

    void AddEnemyCells()
    {   
        levelData.enemyPoses.Clear();
        for (var j = 0; j < Height / 2; j++)
        {
            for (var i = Width - 1; i >= 0; i--)
            {
                levelData.enemyPoses.Add(Nodes[i, j].CellPosition);
            }
        }
    }

    void CreateEnemies()
    {
        foreach (var meleeEnemy in levelData.MeleeEnemies)
        {
            var randomPosition = levelData.enemyPoses[Random.Range(levelData.enemyPoses.Count/2, levelData.enemyPoses.Count)];
            var enemy = Instantiate(meleeEnemy, randomPosition, Quaternion.identity);
            levelData.enemyPoses.Remove(randomPosition);
            if (PrefabObject != null)
            {
                enemy.GetComponent<MeleeEnemy>().enabled = true;
                enemy.GetComponent<Enemy>().enabled = true;
            }
        }
        
        foreach (var rangeEnemy in levelData.RangedEnemies)
        {
            var randomPosition = levelData.enemyPoses[Random.Range(0, levelData.enemyPoses.Count / 2)];
            var enemy = Instantiate(rangeEnemy, randomPosition, Quaternion.identity);
            levelData.enemyPoses.Remove(randomPosition);
            if (PrefabObject != null)
            {
                enemy.GetComponent<RangeEnemy>().enabled = true;
                enemy.GetComponent<Enemy>().enabled = true;
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
                PrefabObject.transform.position = node.CellPosition;
                node.IsPlaceable = false;
                node.Tag = PrefabObject.tag;
                
                var particle = Instantiate(SpawnParticles, node.CellPosition, Quaternion.Euler(-90, 0, 0));
                particle.Play();
                Destroy(particle.gameObject, 1);
            }
            PrefabObject = null;
        }
    }

    public void CreateMeleeSoldier()
    {
        var node = FindEmptyCell();
        if (PrefabObject == null && node != null)
        {
            var soldierPrice = CostManager.Instance.GetSoldierPrice(CostManager.SoldierType.Melee);
            if (CostManager.Instance.CheckMoneyAmount(soldierPrice))
                CostManager.Instance.BuyMeleeSoldier(soldierPrice);
            else
                return;
            PrefabObject = Instantiate(PrefabManager.Instance.Melee1, transform.position, Quaternion.identity * new Quaternion(0, -1, 0, 0));
            var player = PrefabObject;
        }
    }
    
    public void CreateRangeSoldier()
    {
        var node = FindEmptyCell();
        if (PrefabObject == null && node != null)
        {
            var soldierPrice = CostManager.Instance.GetSoldierPrice(CostManager.SoldierType.Ranged);
            if (CostManager.Instance.CheckMoneyAmount(soldierPrice))
                CostManager.Instance.BuyRangedSoldier(soldierPrice);
            else
                return;
            PrefabObject = Instantiate(PrefabManager.Instance.Archer1, transform.position, Quaternion.identity * new Quaternion(0, -1, 0, 0));
            var player = PrefabObject;
        }
    }
}
