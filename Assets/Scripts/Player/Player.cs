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
        public float checkRadius;
        public LayerMask checkLayers;
        public Animator anim;
        public Transform enemy;
        public float health;
        public float armor;
        public float attack;
        public bool lockObj = false;
        public int enemyCount;
        public bool isDead = false;
        NavMeshAgent navMeshAgent;

        private void Awake()
        {
            // If both the base Player AND a subclass (MeleePlayer/RangePlayer) are attached
            // to the same GameObject, the base component must remove itself.
            // Otherwise both scripts run simultaneously causing split-brain: only one instance's
            // health is reduced to 0, but the subclass Die() override is never called.
            if (this.GetType() == typeof(Player))
            {
                bool hasSubclass = GetComponent<MeleePlayer>() != null || GetComponent<RangePlayer>() != null;
                if (hasSubclass)
                {
                    Debug.Log("[CombatSystem] Duplicate Player components found on " + gameObject.name + ". Destroying base Player component to avoid split-brain.");
                    Destroy(this);
                    return;
                }
            }
        }

        public void Update()
        {
            if (isDead) return;

            if (health <= 0)
            {
                Die();
                return;
            }

            enemyCount = GameManager.Instance.CheckEnemyCount();

            if (enemy != null)
            {
                if (enemy.gameObject == null)
                {
                    enemy = null;
                    lockObj = false;
                }
                else
                {
                    var targetEnemy = enemy.GetComponent<Enemy>();
                    if (targetEnemy == null || targetEnemy.isDead || targetEnemy.health <= 0)
                    {
                        enemy = null;
                        lockObj = false;
                    }
                }
            }

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

            if (enemy == null || !lockObj)
            {
                ForceIdle();
            }

            if (enemy != null && GameManager.Instance.isStarted)
            {
                if(gameObject.GetComponent<NavMeshAgent>() == null)
                    gameObject.AddComponent<NavMeshAgent>();
                navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
                navMeshAgent.baseOffset = 0;
                navMeshAgent.radius = 0.1f;
                navMeshAgent.stoppingDistance = 1f;
                //navMeshAgent.enabled = true;
                UpdatePlayer(enemy);
            }
        }

        public virtual void Die()
        {
            Debug.Log("[CombatSystem] Base Player.Die() called for " + gameObject.name + ", isDead was: " + isDead + ", health: " + health);
            if (isDead) return;
            isDead = true;
            navMeshAgent = GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = true;
            }
            // Fallback death sequence in case no subclass override runs
            if (anim != null) anim.SetBool("isDie", true);
            var col = GetComponent<BoxCollider>();
            if (col != null) col.enabled = false;
            Destroy(gameObject, 3f);
        }

        public virtual void ForceIdle()
        {
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
            transform.LookAt(enemySol.position);
            navMeshAgent.isStopped = true;
            transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
            
            var targetEnemy = enemy.GetComponent<Enemy>();
            if (targetEnemy != null)
            {
                float hit = attack - targetEnemy.armor;
                targetEnemy.health -= hit * Time.deltaTime;
            }
        }
    }
}

