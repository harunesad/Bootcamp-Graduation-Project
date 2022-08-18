using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern
{
    public class MeleePlayer : Player
    {
        PlayerState MeleePlayerMode = PlayerState.Idle;
        public Animator anim;
        
     


        //public MeleePlayer(Transform meleePlayerSol)
        //{
        //    base.playerSol = meleePlayerSol;
        //}


        //Update the player enemy's state
        public override void UpdatePlayer(Transform enemySol)
        {
            //The distance between the melee player and the enemy
            //float distance = (transform.position - enemySol.position).magnitude;

            switch (MeleePlayerMode)
            {
                case PlayerState.Idle:
                    MeleePlayerMode = PlayerState.Lock;
                    break;
                case PlayerState.Lock:
                    //if (health == 0)
                    //{
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        MeleePlayerMode = PlayerState.Die;
                    }
                    if (base.enemy.GetComponent<Enemy>().health == 0)
                    {
                        base.lockObj = false;
                        MeleePlayerMode = PlayerState.Lock;
                    }
                    if (base.health != 0 && base.enemy.GetComponent<Enemy>().health != 0)
                    {
                        anim.SetBool("isRun", true);
                        MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                    }
                    //}
                    break;
                case PlayerState.MoveTowardsEnemy:
                    float distance = (transform.position - enemySol.position).magnitude;
                    //float distance = Vector3.Distance(transform.position, enemySol.position);
                    Debug.Log(distance);
                    if (distance < 0.75f)
                    {
                        anim.SetBool("isAttack", true);
                        anim.SetBool("isRun", false);
                        MeleePlayerMode = PlayerState.Attack;
                    }
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        MeleePlayerMode = PlayerState.Die;
                    }
                    if (base.enemy.GetComponent<Enemy>().health == 0)
                    {
                        anim.SetBool("isRun", false);
                        base.lockObj = false;
                        MeleePlayerMode = PlayerState.Lock;
                    }
                    break;
                case PlayerState.Attack:
                    Debug.Log("z");
                    if (base.health == 0)
                    {
                        //gameObject.GetComponent<BoxCollider>().enabled = false;
                        //Destroy(gameObject, 2);
                        anim.SetBool("isDie", true);
                        MeleePlayerMode = PlayerState.Die; 
                    }
                    else if (base.enemy.GetComponent<Enemy>().health == 0)
                    {
                        anim.SetBool("isAttack", false);
                        base.lockObj = false;
                        MeleePlayerMode = PlayerState.Lock;
                    }
                    break;
            }
            //if (enemySol != null)
            //{
                DoAction(enemySol, MeleePlayerMode);
            //}
        }
    }
}

