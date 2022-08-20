using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class RangePlayer : Player
    {
        PlayerState RangePlayerMode = PlayerState.Idle;
        public Animator anim;
        
        public override void UpdatePlayer(Transform enemySol)
        {
            switch (RangePlayerMode)
            {
                case PlayerState.Idle:
                    RangePlayerMode = PlayerState.Lock;
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
            Destroy(gameObject, 4f);
        }

        private void Attack()
        {
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
    }
}
