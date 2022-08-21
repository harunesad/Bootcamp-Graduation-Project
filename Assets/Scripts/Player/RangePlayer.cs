using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class RangePlayer : Player
    {
        public PlayerState RangePlayerMode = PlayerState.Idle;
        
        public override void UpdatePlayer(Transform enemySol)
        {
            switch (RangePlayerMode)
            {
                case PlayerState.Idle:
                    Idle();
                    break;
                case PlayerState.Lock:
                    Lock();
                    break;
                case PlayerState.Attack:
                    Attack();
                    break;
                case PlayerState.Die:
                    Die();
                    break;
            }
            DoAction(enemySol, RangePlayerMode);
        }

        private void Die()
        {
            anim.SetBool("isDie", true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 3f);
        }

        private void Attack()
        {
            if (lockObj == false)
            {
                RangePlayerMode = PlayerState.Idle;
                return;
            }
            if (health < 0)
                RangePlayerMode = PlayerState.Die;
            if (enemy.GetComponent<Enemy>().health < 0)
                lockObj = false;
        }

        private void Lock()
        {
            if (health < 0)
                RangePlayerMode = PlayerState.Die;
            if (enemy.GetComponent<Enemy>().health < 0)
            {
                lockObj = false;
                RangePlayerMode = PlayerState.Attack;
            }

            if (health > 0 && enemy.GetComponent<Enemy>().health > 0)
            {
                anim.SetBool("isThrow", true);
                RangePlayerMode = PlayerState.Attack;
            }
        }

        private void Idle()
        {
            anim.SetBool("isThrow", false);
            if (enemyCount > 1)
                RangePlayerMode = PlayerState.Lock;
        }
    }
}
