using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class RangeEnemy : Enemy
    {
        EnemyState RangeEnemyMode = EnemyState.Idle;

        float health = 100f;

        public RangeEnemy(Transform rangeEnemySol)
        {
            base.enemySol = rangeEnemySol;
        }
        //Update the melee enemy's state
        public override void UpdateEnemy(Transform playerSol, float playerHealth)
        {
            //The distance between the melee enemy and the player
            float distance = (base.enemySol.position - playerSol.position).magnitude;

            switch (RangeEnemyMode)
            {
                case EnemyState.Idle:
                    //if tap to start
                    RangeEnemyMode = EnemyState.Lock;
                    break;
                case EnemyState.Lock:
                    RangeEnemyMode = EnemyState.Attack;
                    break;
                case EnemyState.Attack:
                    if (health == 0)
                    {
                        RangeEnemyMode = EnemyState.Die;
                    }
                    else if (playerHealth == 0)
                    {
                        RangeEnemyMode = EnemyState.Idle;
                    }
                    break;
            }
            DoAction(playerSol, playerHealth, RangeEnemyMode);
        }
    }
}
