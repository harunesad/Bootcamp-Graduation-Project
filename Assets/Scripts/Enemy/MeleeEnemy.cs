using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern
{
    public class MeleeEnemy : Enemy
    {
        EnemyState MeleeEnemyMode = EnemyState.Idle;
        public Animator anim;

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

        private void Die()
        {
            anim.SetBool("isDie", true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 3f);
        }

        private void Attack()
        {
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
            MeleeEnemyMode = EnemyState.Lock;
        }
    }
}

