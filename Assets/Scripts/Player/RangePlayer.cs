using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class RangePlayer : Player
    {
        PlayerState RangePlayerMode = PlayerState.Idle;
        public Animator anim;

        //float health = 100f;


        //public RangePlayer(Transform rangePlayerSol)
        //{
        //    base.playerSol = rangePlayerSol;
        //}


        //Update the player enemy's state
        public override void UpdatePlayer(Transform enemySol)
        {
            //The distance between the range player and the enemy
            //float distance = (base.playerSol.position - enemySol.position).magnitude;

            switch (RangePlayerMode)
            {
                case PlayerState.Idle:
                    RangePlayerMode = PlayerState.Lock;
                    break;
                case PlayerState.Lock:
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        RangePlayerMode = PlayerState.Die;
                    }
                    if (base.enemy.GetComponent<Enemy>().health == 0)
                    {
                        base.lockObj = false;
                        RangePlayerMode = PlayerState.Lock;
                    }
                    if (base.health != 0 && base.enemy.GetComponent<Enemy>().health != 0)
                    {
                        anim.SetBool("isThrow", true);
                        RangePlayerMode = PlayerState.Attack;
                    }
                    //RangePlayerMode = PlayerState.Attack;
                    break;
                case PlayerState.Attack:
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        RangePlayerMode = PlayerState.Die;
                    }
                    else if (base.enemy.GetComponent<Enemy>().health == 0)
                    {
                        base.lockObj = false;
                        anim.SetBool("isThrow", false);
                        RangePlayerMode = PlayerState.Lock;
                    } 
                    break;
            }
            DoAction(enemySol, RangePlayerMode);
        }
    }
}
