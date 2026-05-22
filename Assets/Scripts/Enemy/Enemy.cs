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
        public Animator anim;
        public Transform player;
        public float health;
        public float armor;
        public float attack;
        public bool lockObj = false;
        public int playerCount;
        public bool isDead = false;
        NavMeshAgent navMeshAgent;

        private void Awake()
        {
            // If both the base Enemy AND a subclass (MeleeEnemy/RangeEnemy) are attached
            // to the same GameObject, the base component must remove itself.
            // Otherwise both scripts run simultaneously causing split-brain: only one instance's
            // health is reduced to 0, but the subclass Die() override is never called.
            if (this.GetType() == typeof(Enemy))
            {
                bool hasSubclass = GetComponent<MeleeEnemy>() != null || GetComponent<RangeEnemy>() != null;
                if (hasSubclass)
                {
                    Debug.Log("[CombatSystem] Duplicate Enemy components found on " + gameObject.name + ". Destroying base Enemy component to avoid split-brain.");
                    Destroy(this);
                    return;
                }
            }
        }

        private void Update()
        {
            if (isDead) return;

            if (health <= 0)
            {
                Debug.Log("[CombatSystem] Enemy " + gameObject.name + " health is " + health + " (<= 0). Triggering Die().");
                Die();
                return;
            }

            var players = GameManager.Instance.CheckSoldierCount();
            playerCount = players.Count;

            if (player != null)
            {
                if (player.gameObject == null)
                {
                    player = null;
                    lockObj = false;
                }
                else
                {
                    var targetPlayer = player.GetComponent<Player>();
                    if (targetPlayer == null || targetPlayer.isDead || targetPlayer.health <= 0)
                    {
                        player = null;
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
                    player = item.transform;
                    lockObj = true;
                    break;
                }
                
                if (colliders.Length == 0)
                    lockObj = false;
            }

            if (player == null || !lockObj)
            {
                ForceIdle();
            }
            
            if (player != null && GameManager.Instance.isStarted)
            {
                if(gameObject.GetComponent<NavMeshAgent>() == null)
                    gameObject.AddComponent<NavMeshAgent>();
                navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
                navMeshAgent.baseOffset = 0;
                navMeshAgent.radius = 0.1f;
                navMeshAgent.stoppingDistance = 1f;
                UpdateEnemy(player);
            }
        }

        public virtual void Die()
        {
            Debug.Log("[CombatSystem] Base Enemy.Die() called for " + gameObject.name + ", isDead was: " + isDead + ", health: " + health);
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
            transform.rotation = Quaternion.LookRotation(playerSol.position - transform.position);
            
            var targetPlayer = player.GetComponent<Player>();
            if (targetPlayer != null)
            {
                float hit = attack - targetPlayer.armor;
                targetPlayer.health -= hit * Time.deltaTime;
            }
        }
    }
}

