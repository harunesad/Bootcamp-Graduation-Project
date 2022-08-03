using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class MeleeEnemy : Enemy
    {
        EnemyState MeleeEnemyMode = EnemyState.Idle;

        float health = 100f;


        public MeleeEnemy(Transform meleeEnemySol)
        {
            base.enemySol = meleeEnemySol;
        }


        //Update the melee enemy's state
        public override void UpdateEnemy(Transform playerSol, float playerHealth)
        {
            //The distance between the melee enemy and the player
            float distance = (base.enemySol.position - playerSol.position).magnitude;

            switch (MeleeEnemyMode)
            {
                case EnemyState.Idle:
                    //if tap to start
                    MeleeEnemyMode = EnemyState.Lock;
                    break;
                case EnemyState.Lock:
                    MeleeEnemyMode = EnemyState.MoveTowardsPlayer;
                    break;
                case EnemyState.MoveTowardsPlayer:
                    if (health == 0)
                    {
                        MeleeEnemyMode = EnemyState.Die;
                    }
                    else if (distance < 2f)
                    {
                        MeleeEnemyMode = EnemyState.Attack;
                    }
                    break;
                case EnemyState.Attack:
                    if (health == 0)
                    {
                        MeleeEnemyMode = EnemyState.Die;
                    }
                    else if (playerHealth == 0)
                    {
                        MeleeEnemyMode = EnemyState.Idle;
                    }
                    break;
            }
            DoAction(playerSol, playerHealth, MeleeEnemyMode);
        }
    }
}

