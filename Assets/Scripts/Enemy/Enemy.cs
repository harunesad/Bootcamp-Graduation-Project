using System;
using System.Collections.Generic;
using UnityEngine;
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
                    break;
                case EnemyState.Attack:
                    float hit = attack - player.GetComponent<Player>().armor;
                    if (player.GetComponent<MeleePlayer>() != null)
                        player.GetComponent<MeleePlayer>().health -= hit * Time.deltaTime;
                    if(player.GetComponent<RangePlayer>() != null)
                        player.GetComponent<RangePlayer>().health -= hit * Time.deltaTime;
                    player.GetComponent<Player>().health -= hit * Time.deltaTime;
                    transform.rotation = Quaternion.LookRotation(playerSol.position - transform.position);
                    break;
                case EnemyState.Die:
                    break;
                case EnemyState.MoveTowardsPlayer:
                    transform.rotation = Quaternion.LookRotation(playerSol.position - transform.position);
                    transform.position = Vector3.Lerp(transform.position, playerSol.position, Time.deltaTime);
                    break;
                case EnemyState.Lock:
                    break;
            }
        }
    }
}

