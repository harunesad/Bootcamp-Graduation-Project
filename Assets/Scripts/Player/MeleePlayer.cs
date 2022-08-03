using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class MeleePlayer : Player
    {
        PlayerState MeleePlayerMode = PlayerState.Idle;

        float health = 100f;


        public MeleePlayer(Transform meleePlayerSol)
        {
            base.playerSol = meleePlayerSol;
        }


        //Update the player enemy's state
        public override void UpdatePlayer(Transform enemySol)
        {
            //The distance between the melee player and the enemy
            float distance = (base.playerSol.position - enemySol.position).magnitude;

            switch (MeleePlayerMode)
            {
                case PlayerState.Idle:
                    MeleePlayerMode = PlayerState.Lock;
                    break;
                case PlayerState.Lock:
                    MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                    break;
                case PlayerState.MoveTowardsEnemy:
                    if (health == 0)
                    {
                        MeleePlayerMode = PlayerState.Die;
                    }
                    else if (distance < 2f)
                    {
                        MeleePlayerMode = PlayerState.Attack;
                    }
                    break;
                case PlayerState.Attack:
                    if (health == 0)
                    {
                        MeleePlayerMode = PlayerState.Die; 
                    }
                    break;
            }
            DoAction(enemySol, MeleePlayerMode);
        }
    }
}

