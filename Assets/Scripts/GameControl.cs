using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class GameControl : MonoBehaviour
    {
        public GameObject meleePlayer;
        public GameObject rangePlayer;
        public GameObject meleeEnemy;
        public GameObject rangeEnemy;

        float health = 100;

        //A list that will hold all enemies
        List<Enemy> enemySoldiers = new List<Enemy>();
        //List<Player> playerSoldiers = new List<Player>();

        void Start()
        {
            //Add the enemies we have
            enemySoldiers.Add(new MeleeEnemy(meleeEnemy.transform));
            enemySoldiers.Add(new RangeEnemy(rangeEnemy.transform));
        }


        void Update()
        {
            //Update all enemies to see if they should change state and move/attack player
            for (int i = 0; i < enemySoldiers.Count; i++)
            {
                enemySoldiers[i].UpdateEnemy(meleePlayer.transform, health);
            }
        }
    }
}
