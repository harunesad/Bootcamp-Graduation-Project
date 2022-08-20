using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace StatePattern
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] float checkRadius;
        [SerializeField] LayerMask checkLayers;
        public Transform player;
        public float health;
        public float armor;
        public float attack;
        public bool lockObj = false;
        NavMeshAgent navMeshAgent;

        private void Update()
        {
            if (!lockObj || !Ready.Instance.isReady)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                Array.Sort(colliders, new DistanceCompare(transform));
                foreach (var item in colliders)
                {
                    player = item.transform;
                    lockObj = true;
                    break;
                }
            }
            if (player == null)
            {
                lockObj = false;
            }
            if (player != null && GameManager.Instance.isStarted)
            {
                gameObject.AddComponent<NavMeshAgent>();
                navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
                navMeshAgent.baseOffset = 0;
                navMeshAgent.radius = 0.1f;
                UpdateEnemy(player);
            }
            float attackSpeed = 5f;
        }

        protected enum EnemyState
        {
            Idle,
            Attack,
            Die,
            MoveTowardsPlayer,
            Lock
        }
        
        public virtual void UpdateEnemy(Transform playerSol)
        {

        }
        
        protected void DoAction(Transform playerSol, EnemyState enemyMode)
        {
            switch (enemyMode)
            {
                case EnemyState.Idle:
                    navMeshAgent.isStopped = true;
                    break;
                case EnemyState.Attack:
                    Attack(playerSol);
                    break;
                case EnemyState.Die:
                    navMeshAgent.isStopped = true;
                    break;
                case EnemyState.MoveTowardsPlayer:
                    MoveTowards(playerSol);
                    break;
                case EnemyState.Lock:
                    navMeshAgent.isStopped = true;
                    break;
            }
        }

        private void MoveTowards(Transform playerSol)
        {
            navMeshAgent.isStopped = false;
            transform.rotation = Quaternion.LookRotation(playerSol.position - transform.position);
            //transform.position = Vector3.Lerp(transform.position, playerSol.position, Time.deltaTime);
            navMeshAgent.SetDestination(playerSol.position);
        }

        private void Attack(Transform playerSol)
        {
            navMeshAgent.isStopped = true;
            float hit = attack - player.GetComponent<Player>().armor;
            if (player.GetComponent<MeleePlayer>() != null)
                player.GetComponent<MeleePlayer>().health -= hit * Time.deltaTime;
            if (player.GetComponent<RangePlayer>() != null)
                player.GetComponent<RangePlayer>().health -= hit * Time.deltaTime;
            player.GetComponent<Player>().health -= hit * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(playerSol.position - transform.position);
        }
    }
}

