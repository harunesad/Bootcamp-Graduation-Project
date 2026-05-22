using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern
{
    public class MeleeEnemy : Enemy
    {
        EnemyState MeleeEnemyMode = EnemyState.Idle;

        public override void UpdateEnemy(Transform playerSol)
        {
            switch (MeleeEnemyMode)
            {
                case EnemyState.Idle:
                    Idle();
                    break;
                case EnemyState.Lock:
                    Lock();
                    break;
                case EnemyState.MoveTowardsPlayer:
                    MoveTowards(playerSol);
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Die:
                    Die();
                    break;
            }
                DoAction(playerSol, MeleeEnemyMode);
        }

        public override void Die()
        {
            Debug.Log("[CombatSystem] MeleeEnemy.Die() override called for " + gameObject.name + ", state: " + MeleeEnemyMode);
            if (MeleeEnemyMode == EnemyState.Die) return;
            base.Die();
            MeleeEnemyMode = EnemyState.Die;
            if (anim != null) anim.SetBool("isDie", true);
            var col = gameObject.GetComponent<BoxCollider>();
            if (col != null) col.enabled = false;
            Destroy(gameObject, 3f);
        }

        public override void ForceIdle()
        {
            if (MeleeEnemyMode == EnemyState.Idle) return;
            MeleeEnemyMode = EnemyState.Idle;
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
        }

        private void Attack()
        {
            if (lockObj == false)
            {
                MeleeEnemyMode = EnemyState.Idle;
                return;
            }
            if (health < 0)
                MeleeEnemyMode = EnemyState.Die;
            if (player.GetComponent<Player>().health < 0)
            {
                player.GetComponent<BoxCollider>().enabled = false;
                anim.SetBool("isAttack", false);
                anim.SetBool("isRun", true);
                lockObj = false;
                MeleeEnemyMode = EnemyState.MoveTowardsPlayer;
            }
        }

        private void MoveTowards(Transform playerSol)
        {
            if (lockObj == false)
            {
                MeleeEnemyMode = EnemyState.Idle;
                return;
            }
            float distance = (transform.position - playerSol.position).magnitude;
            if (distance < 0.75f)
            {
                anim.SetBool("isAttack", true);
                anim.SetBool("isRun", false);
                MeleeEnemyMode = EnemyState.Attack;
            }

            if (player.GetComponent<Player>().health < 0)
            {
                anim.SetBool("isRun", true);
                anim.SetBool("isAttack", false);
                lockObj = false;
            }

            if (health < 0)
                MeleeEnemyMode = EnemyState.Die;
        }

        private void Lock()
        {
            if (health < 0)
                MeleeEnemyMode = EnemyState.Die;
            if (player.GetComponent<Player>().health < 0)
            {
                player.GetComponent<BoxCollider>().enabled = false;
                lockObj = false;
            }

            if (health > 0 && player.GetComponent<Player>().health > 0)
            {
                anim.SetBool("isRun", true);
                MeleeEnemyMode = EnemyState.MoveTowardsPlayer;
            }
        }

        private void Idle()
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
            if (playerCount >= 1)
                MeleeEnemyMode = EnemyState.Lock;
        }
    }
}

