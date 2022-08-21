using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierInfo : GenericSingleton<SoldierInfo>
{
    public int count;
    public List<int> counts;
    public GameObject bg;
    public List<GameObject> gridObjects;
    void Start()
    {
        
    }
    void Update()
    {
        counts[0] = GameObject.FindGameObjectsWithTag("Melee 1").Length;
        counts[1] = GameObject.FindGameObjectsWithTag("Melee 2").Length;
        counts[2] = GameObject.FindGameObjectsWithTag("Melee 3").Length;
        counts[3] = GameObject.FindGameObjectsWithTag("Archer 1").Length;
        counts[4] = GameObject.FindGameObjectsWithTag("Archer 2").Length;
        counts[5] = GameObject.FindGameObjectsWithTag("Archer 3").Length;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bg.activeSelf)
            {
                bg.gameObject.SetActive(false);
            }
            else
            {
                //for (int i = 0; i < counts.Count; i++)
                //{
                //    if (counts[i] == 1 && !GridManager.Instance.grids[i].GetComponent<SpawnControl>().spawn)
                //    {
                //        //var gridObj = Instantiate(GridManager.Instance.grids[i]);
                //        //gridObj.transform.parent = GridManager.Instance.grid.transform;
                //        GridManager.Instance.grids[i].SetActive(true);
                //        //gridObjects.Add(gridObj);
                //        Debug.Log("sss");
                //        if (GridManager.Instance.grids[i].activeSelf)
                //        {
                //            GridManager.Instance.grids[i].GetComponent<SpawnControl>().spawn = true;
                //        }
                //    }
                //}
                bg.gameObject.SetActive(true);
            }
        }
        for (int i = 0; i < counts.Count; i++)
        {
            if (counts[i] == 1 && !GridManager.Instance.grids[i].GetComponent<SpawnControl>().spawn)
            {
                //var gridObj = Instantiate(GridManager.Instance.grids[i]);
                //gridObj.transform.parent = GridManager.Instance.grid.transform;
                GridManager.Instance.grids[i].SetActive(true);
                //gridObjects.Add(gridObj);
                Debug.Log("sss");
                if (GridManager.Instance.grids[i].activeSelf)
                {
                    GridManager.Instance.grids[i].GetComponent<SpawnControl>().spawn = true;
                }
            }
        }
    }
}
