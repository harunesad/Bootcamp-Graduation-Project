using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        //The different states the player can be in
        //public Animator anim;
        public float distance;
        bool lockObj = false;
        //private void Awake()
        //{
        //    anim = GetComponent<Animator>();
        //}
        private void Update()
        {
            if (!lockObj)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                Array.Sort(colliders, new DistanceCompare(transform));
                foreach (var item in colliders)
                {
                    enemy = item.transform;
                    lockObj = true;
                    break;
                }
            }
            if (enemy == null)
            {
                lockObj = false;
            }
            //health enemy health olarak de�i�ecek
            if (enemy != null && StartGame.Instance.isStarted)
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
                    enemy.GetComponent<Enemy>().health -= Time.deltaTime;
                    break;
                case PlayerState.Die:
                    //Die player
                    break;
                case PlayerState.MoveTowardsEnemy:
                    //Look at the enemy
                    //anim.SetBool("isRun", true);
                    transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
                    //Move
                    //playerSol.Translate(playerSol.forward * attackSpeed * Time.deltaTime);
                    transform.position = Vector3.Lerp(transform.position, enemySol.position, Time.deltaTime / 5);
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

