using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class MeleeEnemy : Enemy
    {
        EnemyState MeleeEnemyMode = EnemyState.Idle;


        //public MeleeEnemy(Transform meleeEnemySol)
        //{
        //    base.enemySol = meleeEnemySol;
        //}


        //Update the melee enemy's state
        public override void UpdateEnemy(Transform playerSol, float playerHealth)
        {
            //The distance between the melee enemy and the player
            //float distance = (base.enemySol.position - playerSol.position).magnitude;

            switch (MeleeEnemyMode)
            {
                case EnemyState.Idle:
                    //if tap to start
                    MeleeEnemyMode = EnemyState.Lock;
                    break;
                case EnemyState.Lock:
                    if (base.health == 0)
                    {
                        MeleeEnemyMode = EnemyState.Die;
                    }
                    if (playerHealth == 0)
                    {
                        MeleeEnemyMode = EnemyState.Idle;
                    }
                    if (base.health != 0 && playerHealth != 0)
                    {
                        MeleeEnemyMode = EnemyState.MoveTowardsPlayer;
                    }
                    //MeleeEnemyMode = EnemyState.MoveTowardsPlayer;
                    break;
                case EnemyState.MoveTowardsPlayer:
                    float distance = (transform.position - playerSol.position).magnitude;
                    if (distance < 0.75f)
                    {
                        MeleeEnemyMode = EnemyState.Attack;
                    }
                    if (base.health == 0)
                    {
                        MeleeEnemyMode = EnemyState.Die;
                    }
                    if (playerHealth == 0)
                    {
                        MeleeEnemyMode = EnemyState.Idle;
                    }
                    break;
                case EnemyState.Attack:
                    if (base.health == 0)
                    {
                        MeleeEnemyMode = EnemyState.Die;
                    }
                    else if (playerHealth == 0)
                    {
                        MeleeEnemyMode = EnemyState.Idle;
                    }
                    break;
            }
            //if (playerSol != null)
            //{
                DoAction(playerSol, playerHealth, MeleeEnemyMode);
            //}
        }
    }
}

