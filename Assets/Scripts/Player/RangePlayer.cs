using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class RangePlayer : Player
    {
        PlayerState RangePlayerMode = PlayerState.Idle;

        float health = 100f;


        public RangePlayer(Transform rangePlayerSol)
        {
            base.playerSol = rangePlayerSol;
        }


        //Update the player enemy's state
        public override void UpdatePlayer(Transform enemySol, float enemyHealth)
        {
            //The distance between the range player and the enemy
            float distance = (base.playerSol.position - enemySol.position).magnitude;

            switch (RangePlayerMode)
            {
                case PlayerState.Idle:
                    RangePlayerMode = PlayerState.Lock;
                    break;
                case PlayerState.Lock:
                    RangePlayerMode = PlayerState.Attack;
                    break;
                case PlayerState.Attack:
                    if (health == 0)
                    {
                        RangePlayerMode = PlayerState.Die;
                    }
                    else if (enemyHealth == 0)
                    {
                        RangePlayerMode = PlayerState.Idle;
                    } 
                    break;
            }
            DoAction(enemySol, enemyHealth, RangePlayerMode);
        }
    }
}
