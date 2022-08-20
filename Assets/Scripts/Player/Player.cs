using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace StatePattern
{
    public class Player : MonoBehaviour
    {
        public PlayerState MeleePlayerMode = PlayerState.Idle;
        public float checkRadius;
        public LayerMask checkLayers;
        public Animator anim;
        public Transform enemy;
        public float health;
        public float armor;
        public float attack;
        public bool lockObj = false;
        public int count;
        NavMeshAgent navMeshAgent;
        private void Awake()
        {
            //navMeshAgent = GetComponent<NavMeshAgent>();
        }
        //public int count;

        public void Update()
        {
            count = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (!lockObj || !Ready.Instance.isReady)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                Array.Sort(colliders, new DistanceCompare(transform));
                foreach (var item in colliders)
                {
                    enemy = item.transform;
                    lockObj = true;
                    break;
                }

                if (colliders.Length == 0)
                    lockObj = false;
            }

            if (enemy != null && GameManager.Instance.isStarted)
            {
                gameObject.AddComponent<NavMeshAgent>();
                navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
                navMeshAgent.baseOffset = 0;
                navMeshAgent.radius = 0.1f;
                //navMeshAgent.enabled = true;
                UpdatePlayer(enemy);
            }
        }

        public enum PlayerState
        {
            Idle,
            Attack,
            Die,
            MoveTowardsEnemy,
            Lock
        }
        
        public virtual void UpdatePlayer(Transform enemySol)
        {

        }
        
        protected void DoAction(Transform enemySol, PlayerState playerMode)
        {
            switch (playerMode)
            {
                case PlayerState.Idle:
                    navMeshAgent.isStopped = true;
                    //navMeshAgent.enabled = false;
                    break;
                case PlayerState.Attack:
                    Attack(enemySol);
                    break;
                case PlayerState.Die:
                    navMeshAgent.isStopped = true;
                    //navMeshAgent.enabled = false;
                    break;
                case PlayerState.MoveTowardsEnemy:
                    MoveTowards(enemySol);
                    break;
                case PlayerState.Lock:
                    navMeshAgent.isStopped = true;
                    //navMeshAgent.enabled = false;
                    break;
            }
        }

        private void MoveTowards(Transform enemySol)
        {
            transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
            //navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            //transform.position = Vector3.Lerp(transform.position, enemySol.position, Time.deltaTime);
            navMeshAgent.SetDestination(enemySol.position);
        }

        private void Attack(Transform enemySol)
        {
            // if (count == 0)
            // {
            //     MeleePlayerMode = PlayerState.Idle;
            // }
            // transform.rotation = transform.LookAt(enemySol.position - transform.position);
            transform.LookAt(enemySol.position);
            navMeshAgent.isStopped = true;
            //navMeshAgent.enabled = false;
            transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
            float hit = attack - enemy.GetComponent<Enemy>().armor;
            if (enemy.GetComponent<MeleeEnemy>() != null)
                enemy.GetComponent<MeleeEnemy>().health -= hit * Time.deltaTime;
            if (enemy.GetComponent<RangeEnemy>() != null)
                enemy.GetComponent<RangeEnemy>().health -= hit * Time.deltaTime;
            enemy.GetComponent<Enemy>().health -= hit * Time.deltaTime;
        }
    }
}

