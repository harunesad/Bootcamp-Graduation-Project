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
        
        public override void UpdatePlayer(Transform enemySol)
        {
            switch (MeleePlayerMode)
            {
                case PlayerState.Idle:
                    anim.SetBool("isRun",false);
                    anim.SetBool("isAttack", false);
                    if (enemy != null)
                        MeleePlayerMode = PlayerState.Lock;
                    break;
                case PlayerState.Lock:
                    if (health < 0)
                        MeleePlayerMode = PlayerState.Die;
                    if (enemy.GetComponent<Enemy>().health < 0)
                    {
                        enemy.GetComponent<BoxCollider>().enabled = false;
                        lockObj = false;
                    }
                    if (health > 0 && enemy.GetComponent<Enemy>().health > 0)
                    {
                        anim.SetBool("isRun", true);
                        MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                    }
                    break;
                case PlayerState.MoveTowardsEnemy:
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
                    if (health < 0)
                        MeleePlayerMode = PlayerState.Die; 
                    if (GameManager.Instance.isAllEnemiesDead())
                        MeleePlayerMode = PlayerState.Idle;
                    break;
                case PlayerState.Attack:
                    if (GameManager.Instance.isAllEnemiesDead())
                        MeleePlayerMode = PlayerState.Idle;
                    if (health < 0)
                        MeleePlayerMode = PlayerState.Die; 
                    if (enemy.GetComponent<Enemy>().health < 0)
                    {
                        enemy.GetComponent<BoxCollider>().enabled = false;
                        anim.SetBool("isAttack", false);
                        anim.SetBool("isRun", true);
                        lockObj = false;
                        MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                    }
                    float distance1 = (transform.position - enemySol.position).magnitude; 
                    if (distance1 > 0.75f)
                    {
                        anim.SetBool("isAttack", false);
                        anim.SetBool("isRun", true);
                        MeleePlayerMode = PlayerState.MoveTowardsEnemy;
                    } 
                    break;
                case PlayerState.Die:
                    anim.SetBool("isDie", true);
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(gameObject, 4f);
                    break;
            }
                DoAction(enemySol, MeleePlayerMode);
        }
    }
}

