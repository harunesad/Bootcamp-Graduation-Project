using System.Collections;
using System.Collections.Generic;
using StatePattern;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Data", fileName = "Level")]
public class LevelData : ScriptableObject
{
    public int levelCoinCount;
    public List<Vector3> enemyPoses;
    public List<GameObject> MeleeEnemies;
    public List<GameObject> RangedEnemies;
}
