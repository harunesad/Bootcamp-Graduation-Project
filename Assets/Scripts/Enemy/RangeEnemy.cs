using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class RangeEnemy : Enemy
    {
        EnemyState RangeEnemyMode = EnemyState.Idle;

        //float health = 100f;

        //public RangeEnemy(Transform rangeEnemySol)
        //{
        //    base.enemySol = rangeEnemySol;
        //}
        //Update the melee enemy's state
        public override void UpdateEnemy(Transform playerSol, float playerHealth)
        {
            //The distance between the melee enemy and the player
            //float distance = (base.enemySol.position - playerSol.position).magnitude;

            switch (RangeEnemyMode)
            {
                case EnemyState.Idle:
                    //if tap to start
                    RangeEnemyMode = EnemyState.Lock;
                    break;
                case EnemyState.Lock:
                    if (base.health == 0)
                    {
                        RangeEnemyMode = EnemyState.Die;
                    }
                    if (playerHealth == 0)
                    {
                        RangeEnemyMode = EnemyState.Idle;
                    }
                    if (base.health != 0 && playerHealth != 0)
                    {
                        RangeEnemyMode = EnemyState.Attack;
                    }
                    //RangeEnemyMode = EnemyState.Attack;
                    break;
                case EnemyState.Attack:
                    if (base.health == 0)
                    {
                        RangeEnemyMode = EnemyState.Die;
                    }
                    else if (playerHealth == 0)
                    {
                        RangeEnemyMode = EnemyState.Idle;
                    }
                    break;
            }
            //if (playerSol != null)
            //{
                DoAction(playerSol, playerHealth, RangeEnemyMode);
            //}
        }
    }
}
