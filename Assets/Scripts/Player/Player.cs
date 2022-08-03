using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Player : MonoBehaviour
    {
        protected Transform playerSol;

        //The different states the player can be in
        protected enum PlayerState
        {
            Idle,
            Attack,
            Die,
            MoveTowardsEnemy,
            Lock
        }
        //Update the player by giving it a new state
        public virtual void UpdatePlayer(Transform enemySol, float enemyHealth)
        {

        }
        //Do something based on a state
        protected void DoAction(Transform enemySol, float enemyHealth, PlayerState playerMode)
        {
            float attackSpeed = 5f;

            switch (playerMode)
            {
                case PlayerState.Idle:
                    //Idle player
                    break;
                case PlayerState.Attack:
                    //Attack enemy
                    enemyHealth -= 10;
                    break;
                case PlayerState.Die:
                    //Die player
                    break;
                case PlayerState.MoveTowardsEnemy:
                    //Look at the enemy
                    playerSol.rotation = Quaternion.LookRotation(enemySol.position - playerSol.position);
                    //Move
                    playerSol.Translate(playerSol.forward * attackSpeed * Time.deltaTime);
                    break;
                case PlayerState.Lock:
                    //Lock the enemy
                    break;
            }
        }
    }
}

