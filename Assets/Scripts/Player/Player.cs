using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StatePattern
{
    public class Player : MonoBehaviour
    {
        //protected Transform playerSol;
        [SerializeField] float checkRadius;
        [SerializeField] LayerMask checkLayers;
        public Transform enemy;
        public float health;
        public float armor;
        public float attack;
        public float moveSpeed;
        GameManager gameManager = new GameManager();
        //The different states the player can be in
        //public Animator anim;
        public float distance;
        public bool lockObj = false;
        //private void Awake()
        //{
        //    anim = GetComponent<Animator>();
        //}
        private void Update()
        {
            Debug.Log(Ready.Instance.isReady);
            if (!lockObj || !Ready.Instance.isReady)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                Array.Sort(colliders, new DistanceCompare(transform));
                foreach (var item in colliders)
                {
                    Debug.Log("asa");
                    enemy = item.transform;
                    lockObj = true;
                    //Ready.instance.isReady = true;
                    break;
                }
            }
            if (enemy == null)
            {
                lockObj = false;
            }
            //health enemy health olarak de�i�ecek
            if (enemy != null && GameManager.Instance.isStarted)
            {
                //distance = Vector3.Distance(transform.localPosition, enemy.localPosition);
                UpdatePlayer(enemy);
            }
            float attackSpeed = 5f;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
        protected enum PlayerState
        {
            Idle,
            Attack,
            Die,
            MoveTowardsEnemy,
            Lock
        }
        //Update the player by giving it a new state
        public virtual void UpdatePlayer(Transform enemySol)
        {

        }
        //Do something based on a state
        protected void DoAction(Transform enemySol, PlayerState playerMode)
        {
            //float attackSpeed = 5f;
            //Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            //Array.Sort(colliders, new DistanceCompare(transform));
            //foreach (var item in colliders)
            //{
            //    Debug.Log(item.name);
            //    enemy = item.transform;
            //    break;
            //}

            switch (playerMode)
            {
                case PlayerState.Idle:
                    //Idle player
                    break;
                case PlayerState.Attack:
                    transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
                    //Attack enemy
                    //anim.SetBool("isRun", false);
                    //anim.SetBool("isAttack", true);
                    if (enemy.GetComponent<Enemy>().health >= 0)
                    {
                        enemy.GetComponent<Enemy>().health -= 100;
                    }
                    break;
                case PlayerState.Die:
                    //Die player
                    Debug.Log("dddd");
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(gameObject, 2);
                    break;
                case PlayerState.MoveTowardsEnemy:
                    //Look at the enemy
                    //anim.SetBool("isRun", true);
                    transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
                    //Move
                    //playerSol.Translate(playerSol.forward * attackSpeed * Time.deltaTime);
                    transform.position = Vector3.Lerp(transform.position, enemySol.position, Time.deltaTime);
                    break;
                case PlayerState.Lock:
                    //Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                    //Array.Sort(colliders, new DistanceCompare(transform));
                    //foreach (var item in colliders)
                    //{
                    //    Debug.Log(item.name);
                    //    enemySol = item.transform;
                    //    break;
                    //}
                    break;
            }
        }
    }
}

