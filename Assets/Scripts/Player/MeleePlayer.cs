using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern
{
    public class MeleePlayer : Player
    {
        public PlayerState MeleePlayerMode = PlayerState.Idle;
        
        public override void UpdatePlayer(Transform enemySol)
        {
            switch (MeleePlayerMode)
            {
                case PlayerState.Idle:
                    Idle();
                    break;
                case PlayerState.Lock:
                    Lock();
                    break;
                case PlayerState.MoveTowardsEnemy:
                    MoveTowards(enemySol);
                    break;
                case PlayerState.Attack:
                    Attack(enemySol);
                    break;
                case PlayerState.Die:
                    Die();
                    break;
            }
                DoAction(enemySol, MeleePlayerMode);
        }

        public override void Die()
        {
            Debug.Log("[CombatSystem] MeleePlayer.Die() override called for " + gameObject.name + ", state: " + MeleePlayerMode);
            if (MeleePlayerMode == PlayerState.Die) return;
            base.Die();
            MeleePlayerMode = PlayerState.Die;
            if (anim != null) anim.SetBool("isDie", true);
            var col = gameObject.GetComponent<BoxCollider>();
            if (col != null) col.enabled = false;
            Destroy(gameObject, 3f);
        }

        public override void ForceIdle()
        {
            if (MeleePlayerMode == PlayerState.Idle) return;
            MeleePlayerMode = PlayerState.Idle;
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
        }

        private void Attack(Transform enemySol)
        {
            if (lockObj == false)
            {
                MeleePlayerMode = PlayerState.Idle;
                return;
            }
            if (health < 0)
                MeleePlayerMode = PlayerState.Die;
            if (enemy != null)
            {
                if (enemy.GetComponent<Enemy>().health < 0)
                {
                    enemy.GetComponent<BoxCollider>().enabled = false;
                    anim.SetBool("isAttack", false);
                    anim.SetBool("isRun", true);
                    MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                }
                float distance = (transform.position - enemySol.position).magnitude;
                if (distance > 2f)
                {
                    anim.SetBool("isAttack", false);
                    anim.SetBool("isRun", true);
                    MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                }
            }
        }

        private void MoveTowards(Transform enemySol)
        {
            if (lockObj == false)
            {
                MeleePlayerMode = PlayerState.Idle;
                return;
            }
            if (enemy != null)
            {
                float distance = (transform.position - enemySol.position).magnitude;
                if (distance < 2f)
                {
                    anim.SetBool("isAttack", true);
                    anim.SetBool("isRun", false);
                    MeleePlayerMode = PlayerState.Attack;
                }

                if (enemy.GetComponent<Enemy>().health < 0)
                {
                    anim.SetBool("isRun", true);
                    anim.SetBool("isAttack", false);
                    lockObj = false;
                }
            }
            if (health < 0)
                MeleePlayerMode = PlayerState.Die;
        }

        private void Lock()
        {
            if (health < 0)
                MeleePlayerMode = PlayerState.Die;
            if (enemy != null && enemy.GetComponent<Enemy>().health < 0)
                lockObj = false;

            if (enemy != null && health > 0 && enemy.GetComponent<Enemy>().health > 0)
            {
                anim.SetBool("isRun", true);
                MeleePlayerMode = PlayerState.MoveTowardsEnemy;
            }
        }

        private void Idle()
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
            if (enemyCount >= 1)
                MeleePlayerMode = PlayerState.Lock;
        }
    }
}

