using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Enemy : MonoBehaviour
    {
        //protected Transform enemySol;
        [SerializeField] float checkRadius;
        [SerializeField] LayerMask checkLayers;
        Transform player;
        public float health;
        public float armor;
        public float attack;
        public float moveSpeed;

        private void Update()
        {
            //health player health olarak 
            if (player != null && StartGame.Instance.isStarted)
            {
                UpdateEnemy(player, player.GetComponent<Player>().health);
            }
            float attackSpeed = 5f;
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            Array.Sort(colliders, new DistanceCompare(transform));
            foreach (var item in colliders)
            {
                player = item.transform;
                break;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
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
            //float attackSpeed = 5f;
            //Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            //Array.Sort(colliders, new DistanceCompare(transform));
            //foreach (var item in colliders)
            //{
            //    Debug.Log(item.name);
            //    player = item.transform;
            //    break;
            //}

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
                    transform.rotation = Quaternion.LookRotation(playerSol.position - transform.position);
                    //Move
                    transform.position = Vector3.Lerp(transform.position, playerSol.position, Time.deltaTime);
                    break;
                case EnemyState.Lock:
                    //Lock the player
                    break;
            }
        }
    }
}

