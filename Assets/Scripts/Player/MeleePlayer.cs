using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern
{
    public class MeleePlayer : Player
    {
        [SerializeField]PlayerState MeleePlayerMode = PlayerState.Idle;
        public Animator anim;
        public int count;

        void Update()
        {
            base.Update();
            count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
        
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

        private void Die()
        {
            anim.SetBool("isDie", true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 4f);
        }

        private void Attack(Transform enemySol)
        {
            if (count == 0)
            {
                MeleePlayerMode = PlayerState.Idle;
                Debug.Log("sdas");
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
                    lockObj = false;
                    MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                }

                float distance = (transform.position - enemySol.position).magnitude;
                if (distance > 0.75f)
                {
                    anim.SetBool("isAttack", false);
                    anim.SetBool("isRun", true);
                    MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                }
            }
        }

        private void MoveTowards(Transform enemySol)
        {
            if (count == 0)
            {
                MeleePlayerMode = PlayerState.Idle;
                Debug.Log("sdas");
            }
            if (enemy != null)
            {
                float distance = (transform.position - enemySol.position).magnitude;
                if (distance < 0.75f)
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
            {
                enemy.GetComponent<BoxCollider>().enabled = false;
                lockObj = false;
            }

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
            if (count != 0)
                MeleePlayerMode = PlayerState.Lock;
        }
    }
}

