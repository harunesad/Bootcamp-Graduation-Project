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
                    break;
                case PlayerState.Attack:
                    if (health < 0)
                        RangePlayerMode = PlayerState.Die;
                    if (enemy.GetComponent<Enemy>().health < 0)
                        lockObj = false;
                    break;
                case PlayerState.Die:
                    anim.SetBool("isDie", true);
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(gameObject, 4f);
                    break;
            }
            DoAction(enemySol, RangePlayerMode);
        }
    }
}
