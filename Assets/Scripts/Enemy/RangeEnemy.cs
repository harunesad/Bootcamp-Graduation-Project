using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class RangeEnemy : Enemy
    {
        EnemyState RangeEnemyMode = EnemyState.Idle;
        public Animator anim;

        public override void UpdateEnemy(Transform playerSol)
        {

            switch (RangeEnemyMode)
            {
                case EnemyState.Idle:
                    RangeEnemyMode = EnemyState.Lock;
                    break;
                case EnemyState.Lock:
                    if (health < 0)
                        RangeEnemyMode = EnemyState.Die;
                    if (player.GetComponent<Player>().health < 0)
                    {
                        lockObj = false;
                        RangeEnemyMode = EnemyState.Attack;
                    }
                    if (health > 0 && base.player.GetComponent<Player>().health > 0)
                    {
                        anim.SetBool("isThrow", true);
                        RangeEnemyMode = EnemyState.Attack;
                    }
                    break;
                case EnemyState.Attack:
                    if (health < 0)
                        RangeEnemyMode = EnemyState.Die;
                    if (player.GetComponent<Player>().health < 0)
                        lockObj = false;
                    break;
                case EnemyState.Die:
                    anim.SetBool("isDie", true);
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(gameObject, 4f);
                    break;
            }
            DoAction(playerSol, RangeEnemyMode);
        }
    }
}
