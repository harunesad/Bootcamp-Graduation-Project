using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class MeleePlayer : Player
    {
        PlayerState MeleePlayerMode = PlayerState.Idle;


        //public MeleePlayer(Transform meleePlayerSol)
        //{
        //    base.playerSol = meleePlayerSol;
        //}


        //Update the player enemy's state
        public override void UpdatePlayer(Transform enemySol, float enemyHealth)
        {
            //The distance between the melee player and the enemy
            //float distance = (transform.position - enemySol.position).magnitude;

            switch (MeleePlayerMode)
            {
                case PlayerState.Idle:
                    MeleePlayerMode = PlayerState.Lock;
                    break;
                case PlayerState.Lock:
                    //if (health == 0)
                    //{
                    if (base.health == 0)
                    {
                        MeleePlayerMode = PlayerState.Die;
                    }
                    if (enemyHealth == 0)
                    {
                        MeleePlayerMode = PlayerState.Idle;
                    }
                    if (base.health != 0 && enemyHealth != 0)
                    {
                        MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                    }
                    //}
                    break;
                case PlayerState.MoveTowardsEnemy:
                    float distance = (transform.position - enemySol.position).magnitude;
                    Debug.Log(distance);
                    if (distance < 0.75f)
                    {
                        MeleePlayerMode = PlayerState.Attack;
                    }
                    if (base.health == 0)
                    {
                        MeleePlayerMode = PlayerState.Die;
                    }
                    if (enemyHealth == 0)
                    {
                        MeleePlayerMode = PlayerState.Idle;
                    }
                    break;
                case PlayerState.Attack:
                    if (base.health == 0)
                    {
                        MeleePlayerMode = PlayerState.Die; 
                    }
                    else if (enemyHealth == 0)
                    {
                        MeleePlayerMode = PlayerState.Idle;
                    }
                    break;
            }
            //if (enemySol != null)
            //{
                DoAction(enemySol, enemyHealth, MeleePlayerMode);
            //}
        }
    }
}

