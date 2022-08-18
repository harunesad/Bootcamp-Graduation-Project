using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class RangeEnemy : Enemy
    {
        EnemyState RangeEnemyMode = EnemyState.Idle;
        public Animator anim;

        //float health = 100f;

        //public RangeEnemy(Transform rangeEnemySol)
        //{
        //    base.enemySol = rangeEnemySol;
        //}
        //Update the melee enemy's state
        public override void UpdateEnemy(Transform playerSol)
        {
            //The distance between the melee enemy and the player
            //float distance = (base.enemySol.position - playerSol.position).magnitude;

            switch (RangeEnemyMode)
            {
                case EnemyState.Idle:
                    //if tap to start
                    RangeEnemyMode = EnemyState.Lock;
                    break;
                case EnemyState.Lock:
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        RangeEnemyMode = EnemyState.Die;
                    }
                    if (base.player.GetComponent<Player>().health == 0)
                    {
                        base.lockObj = false;
                        RangeEnemyMode = EnemyState.Lock;
                    }
                    if (base.health != 0 && base.player.GetComponent<Player>().health != 0)
                    {
                        anim.SetBool("isThrow", true);
                        RangeEnemyMode = EnemyState.Attack;
                    }
                    //RangeEnemyMode = EnemyState.Attack;
                    break;
                case EnemyState.Attack:
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        RangeEnemyMode = EnemyState.Die;
                    }
                    else if (base.player.GetComponent<Player>().health == 0)
                    {
                        base.lockObj = false;
                        anim.SetBool("isThrow", false);
                        RangeEnemyMode = EnemyState.Lock;
                    }
                    break;
            }
            //if (playerSol != null)
            //{
                DoAction(playerSol, RangeEnemyMode);
            //}
        }
    }
}
