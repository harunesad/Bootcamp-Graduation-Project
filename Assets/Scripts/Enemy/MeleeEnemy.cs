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

        //Update the melee enemy's state
        public override void UpdateEnemy(Transform playerSol)
        {
            //The distance between the melee enemy and the player
            //float distance = (base.enemySol.position - playerSol.position).magnitude;

            switch (MeleeEnemyMode)
            {
                case EnemyState.Idle:
                    //if tap to start
                    MeleeEnemyMode = EnemyState.Lock;
                    break;
                case EnemyState.Lock:
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        MeleeEnemyMode = EnemyState.Die;
                    }
                    if (base.player.GetComponent<Player>().health == 0)
                    {
                        base.lockObj = false;
                        MeleeEnemyMode = EnemyState.Lock;
                    }
                    if (base.health != 0 && base.player.GetComponent<Player>().health != 0)
                    {
                        anim.SetBool("isRun", true);
                        MeleeEnemyMode = EnemyState.MoveTowardsPlayer;
                    }
                    //MeleeEnemyMode = EnemyState.MoveTowardsPlayer;
                    break;
                case EnemyState.MoveTowardsPlayer:
                    float distance = (transform.position - playerSol.position).magnitude;
                    if (distance < 0.75f)
                    {
                        anim.SetBool("isAttack", true);
                        anim.SetBool("isRun", false);
                        MeleeEnemyMode = EnemyState.Attack;
                    }
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        MeleeEnemyMode = EnemyState.Die;
                    }
                    if (base.player.GetComponent<Player>().health == 0)
                    {
                        base.lockObj = false;
                        anim.SetBool("isRun", false);
                        MeleeEnemyMode = EnemyState.Lock;
                    }
                    break;
                case EnemyState.Attack:
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        MeleeEnemyMode = EnemyState.Die;
                    }
                    else if (base.player.GetComponent<Player>().health == 0)
                    {
                        base.lockObj = false;
                        anim.SetBool("isAttack", false);
                        MeleeEnemyMode = EnemyState.Lock;
                    }
                    break;
            }
            //if (playerSol != null)
            //{
                DoAction(playerSol, MeleeEnemyMode);
            //}
        }
    }
}

