using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Player : MonoBehaviour
    {
        //protected Transform playerSol;
        [SerializeField] float checkRadius;
        [SerializeField] LayerMask checkLayers;
        Transform enemy;
        public float health;
        public float armor;
        public float attack;
        public float moveSpeed;
        //The different states the player can be in
        
        private void Update()
        {
            //health enemy health olarak de�i�ecek
            if (enemy != null && StartGame.Instance.isStarted)
            {
                UpdatePlayer(enemy, enemy.GetComponent<Enemy>().health);
            }
            float attackSpeed = 5f;
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            Array.Sort(colliders, new DistanceCompare(transform));
            foreach (var item in colliders)
            {
                enemy = item.transform;
                break;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
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
            //float attackSpeed = 5f;
            //Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            //Array.Sort(colliders, new DistanceCompare(transform));
            //foreach (var item in colliders)
            //{
            //    Debug.Log(item.name);
            //    enemy = item.transform;
            //    break;
            //}

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
                    transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
                    //Move
                    //playerSol.Translate(playerSol.forward * attackSpeed * Time.deltaTime);
                    transform.position = Vector3.Lerp(transform.position, enemySol.position, Time.deltaTime);
                    break;
                case PlayerState.Lock:
                    //Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                    //Array.Sort(colliders, new DistanceCompare(transform));
                    //foreach (var item in colliders)
                    //{
                    //    Debug.Log(item.name);
                    //    enemySol = item.transform;
                    //    break;
                    //}
                    break;
            }
        }
    }
}

