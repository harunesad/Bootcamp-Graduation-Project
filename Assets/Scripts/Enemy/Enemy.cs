using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StatePattern
{
    public class Enemy : MonoBehaviour
    {
        //protected Transform enemySol;
        [SerializeField] float checkRadius;
        [SerializeField] LayerMask checkLayers;
        public Transform player;
        public float health;
        public float armor;
        public float attack;
        public float moveSpeed;
        Animator anim;
        public bool lockObj = false;
        GameManager gameManager = new GameManager();
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            //health player health olarak 
            if (!lockObj || !Ready.Instance.isReady)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                Array.Sort(colliders, new DistanceCompare(transform));
                foreach (var item in colliders)
                {
                    player = item.transform;
                    lockObj = true;
                    //Ready.instance.isReady = true;
                    break;
                }
            }
            if (player == null)
            {
                lockObj = false;
            }
            if (player != null && GameManager.Instance.isStarted)
            {
                UpdateEnemy(player);
            }
            float attackSpeed = 5f;
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
        public virtual void UpdateEnemy(Transform playerSol)
        {

        }
        //Do something based on a state
        protected void DoAction(Transform playerSol, EnemyState enemyMode)
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
                    if (player.GetComponent<Player>().health >= 0)
                    {
                        player.GetComponent<Player>().health -= 100;
                    }
                    transform.rotation = Quaternion.LookRotation(playerSol.position - transform.position);
                    break;
                case EnemyState.Die:
                    //Die enemy
                    break;
                case EnemyState.MoveTowardsPlayer:
                    //Look at the player
                    transform.rotation = Quaternion.LookRotation(playerSol.position - transform.position);
                    //Move
                    transform.position = Vector3.Lerp(transform.position, playerSol.position, Time.deltaTime / 5);
                    break;
                case EnemyState.Lock:
                    //Lock the player
                    break;
            }
        }
    }
}

