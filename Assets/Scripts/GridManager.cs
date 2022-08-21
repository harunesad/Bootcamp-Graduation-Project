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
    public int currentLevelIndex;
    
    private GameObject PrefabObject;
    
    void Start()
    {
        currentLevelIndex = GameManager.Instance.levelIndex;
        CreateGrid();
        AddEnemyCells();
        CreateEnemies();
        if(GameManager.Instance.levelIndex != 0) 
            GetJsonSoldiersData();
    }

    void Update()
    {
        if (GameManager.Instance.levelIndex != currentLevelIndex)
        {
            currentLevelIndex = GameManager.Instance.levelIndex;
            AddEnemyCells();
            CreateEnemies();
        }
            
        PutPrefabOnGrid();
    }

    public void NextLevel()
    {
        RemoveSoldiers();
        SetEmptyCells();
        if(GameManager.Instance.levelIndex != 0) 
            GetJsonSoldiersData();
        GameManager.Instance.isStarted = false;
    }
    
    public void RestartLevel()
    {
        SetEmptyCells();
        RemoveSoldiers();
        RemoveEnemies();
        AddEnemyCells();
        CreateEnemies();
        GetJsonSoldiersData();
        GameManager.Instance.isStarted = false;
    }

    public void GetJsonSoldiersData()
    {
        var soldiers = JsonController.Instance.JsonLoad();
        if(soldiers.Count == 0)
            return;
        foreach (var soldier in soldiers)
        {
            var prefabName = soldier.PrefabName.Split("(").GetValue(0);
            var prefabSoldier = Instantiate(PrefabManager.Instance.GetPrefab(prefabName.ToString()), new Vector3(soldier.PosX, 0, soldier.PosZ), Quaternion.identity * new Quaternion(0, -1, 0, 0));
            Nodes[soldier.PosX, soldier.PosZ].IsPlaceable = false;
            Nodes[soldier.PosX, soldier.PosZ].Tag = prefabSoldier.tag;
        }
    }

    void SoldiersPutOnGrid()
    {
        for (var i = 1; i < 4; i++)
        {
            var meleeSoldiers = GameObject.FindGameObjectsWithTag("Melee " + i);
            if (meleeSoldiers != null)
            {
                for (int j = 0; j < meleeSoldiers.Length; j++)
                {
                    var node = FindEmptyCell();
                    meleeSoldiers[j].transform.position = node.CellPosition;
                    node.IsPlaceable = false;
                    node.Tag = meleeSoldiers[j].tag;
                    meleeSoldiers[j].GetComponent<Player>().health = 100;
                    var anim = meleeSoldiers[j].GetComponent<Animator>();
                    anim.SetBool("isRun", false);
                    anim.SetBool("isAttack", false);
                    SetSoldierPosition(meleeSoldiers, j);
                }
            }
        }
        for (var i = 1; i < 4; i++)
        {
            var rangedSoldiers = GameObject.FindGameObjectsWithTag("Archer " + i);
            if (rangedSoldiers != null)
            {
                for (int j = 0; j < rangedSoldiers.Length; j++)
                {
                    var node = FindEmptyCell();
                    rangedSoldiers[j].transform.position = node.CellPosition;
                    node.IsPlaceable = false;
                    node.Tag = rangedSoldiers[j].tag;
                    rangedSoldiers[j].GetComponent<Player>().health = 100;
                    var anim = rangedSoldiers[j].GetComponent<Animator>();
                    anim.SetBool("isThrow", false);
                    SetSoldierPosition(rangedSoldiers, j);
                }
            }
        }
    }

    public List<PlayerGridPosition> GetSoldiersPlayerGridPositions()
    {
        var playerGridPositions = new List<PlayerGridPosition>();
        for (var i = 1; i < 4; i++)
        {
            var meleeSoldiers = GameObject.FindGameObjectsWithTag("Melee " + i);
            if (meleeSoldiers != null)
            {
                for (int j = 0; j < meleeSoldiers.Length; j++)
                {
                    var playerGridPosition = new PlayerGridPosition();
                    playerGridPosition.PosX = meleeSoldiers[j].GetComponent<DragAndDropObject>().LastPosX;
                    playerGridPosition.PosZ = meleeSoldiers[j].GetComponent<DragAndDropObject>().LastPosZ;
                    playerGridPosition.PrefabName = meleeSoldiers[j].name;
                    playerGridPositions.Add(playerGridPosition);
                }
            }
        }
        for (var i = 1; i < 4; i++)
        {
            var rangedSoldiers = GameObject.FindGameObjectsWithTag("Archer " + i);
            if (rangedSoldiers != null)
            {
                for (int j = 0; j < rangedSoldiers.Length; j++)
                {
                    var playerGridPosition = new PlayerGridPosition();
                    playerGridPosition.PosX = rangedSoldiers[j].GetComponent<DragAndDropObject>().LastPosX;
                    playerGridPosition.PosZ = rangedSoldiers[j].GetComponent<DragAndDropObject>().LastPosZ;
                    playerGridPosition.PrefabName = rangedSoldiers[j].name;
                    playerGridPositions.Add(playerGridPosition);
                }
            }
        }
        return playerGridPositions;
    }

    private static void SetSoldierPosition(GameObject[] meleeSoldiers, int j)
    {
        var soldier = meleeSoldiers[j].GetComponent<DragAndDropObject>();
        soldier.LastPosX = (int)meleeSoldiers[j].transform.position.x;
        soldier.LastPosZ = (int)meleeSoldiers[j].transform.position.z;
        soldier.FirstPosX = (int)meleeSoldiers[j].transform.position.x;
        soldier.FirstPosZ = (int)meleeSoldiers[j].transform.position.z;
    }

    void RemoveSoldiers()
    {
        for (var i = 1; i < 4; i++)
        {
            var soldier = GameObject.FindGameObjectsWithTag("Melee " + i);
            if (soldier != null)
            {
                for (int j = 0; j < soldier.Length; j++)
                {
                    Destroy(soldier[j]);
                }
            }
        }
        for (var i = 1; i < 4; i++)
        {
            var soldier = GameObject.FindGameObjectsWithTag("Archer " + i);
            if(soldier != null)
                for (int j = 0; j < soldier.Length; j++)
                {
                    Destroy(soldier[j]);
                }
        }
    }

    void RemoveEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies != null)
        {
            for (int j = 0; j < enemies.Length; j++)
            {
                Destroy(enemies[j]);
            }
        }
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
        PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses.Clear();
        for (var j = 0; j < Height / 2; j++)
        {
            for (var i = Width - 1; i >= 0; i--)
            {
                PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses.Add(Nodes[i, j].CellPosition);
            }
        }
    }

    void CreateEnemies()
    {
        foreach (var meleeEnemy in PrefabManager.Instance.LevelDatas[currentLevelIndex].MeleeEnemies)
        {
            var randomPosition = PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses[Random.Range(PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses.Count/2, PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses.Count)];
            var enemy = Instantiate(meleeEnemy, randomPosition, meleeEnemy.transform.rotation);
            PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses.Remove(randomPosition);
            if (PrefabObject != null)
            {
                enemy.GetComponent<MeleeEnemy>().enabled = true;
                enemy.GetComponent<Enemy>().enabled = true;
            }
        }
        
        foreach (var rangeEnemy in PrefabManager.Instance.LevelDatas[currentLevelIndex].RangedEnemies)
        {
            var randomPosition = PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses[Random.Range(0, PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses.Count / 2)];
            var enemy = Instantiate(rangeEnemy, randomPosition, rangeEnemy.transform.rotation);
            PrefabManager.Instance.LevelDatas[currentLevelIndex].enemyPoses.Remove(randomPosition);
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

    public void SetEmptyCells()
    {
        for (var i = Width - 1; i >= 0; i--)
        {
            for (var j = Height - 1; j >= Height / 2; j--)
            {
                Nodes[i, j].IsPlaceable = true;
                Nodes[i, j].Tag = "";
            }
        }
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
                
                var particle = Instantiate(PrefabManager.Instance.SpawnParticle, node.CellPosition, Quaternion.Euler(-90, 0, 0));
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
        //for (int i = 0; i < SoldierInfo.Instance.counts.Count - 3; i++)
        //{
        //    if (SoldierInfo.Instance.counts[i] == 1)
        //    {
        //        var gridObj = Instantiate(grids[i]);
        //        gridObj.transform.parent = grid.transform;
        //        Debug.Log("sss");
        //    }
        //}
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
