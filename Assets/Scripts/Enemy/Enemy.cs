using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Enemy : MonoBehaviour
    {
        protected Transform enemySol;

        //The different states the enemy can be in
        protected enum EnemyState
        {
            Idle,
            Attack,
            Die,
            MoveTowardsPlayer,
            Lock
        }
        //Update the enemy by giving it a new state
        public virtual void UpdateEnemy(Transform playerSol, float playerHealth)
        {

        }
        //Do something based on a state
        protected void DoAction(Transform playerSol, float playerHealth, EnemyState enemyMode)
        {
            float attackSpeed = 5f;

            switch (enemyMode)
            {
                case EnemyState.Idle:
                    //Idle enemy
                    break;
                case EnemyState.Attack:
                    //Attack player
                    playerHealth -= 10;
                    break;
                case EnemyState.Die:
                    //Die enemy
                    break;
                case EnemyState.MoveTowardsPlayer:
                    //Look at the player
                    enemySol.rotation = Quaternion.LookRotation(playerSol.position - enemySol.position);
                    //Move
                    enemySol.Translate(enemySol.forward * attackSpeed * Time.deltaTime);
                    break;
                case EnemyState.Lock:
                    //Lock the player
                    break;
            }
        }
    }
}

