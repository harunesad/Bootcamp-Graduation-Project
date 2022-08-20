using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StatePattern
{
    public class Player : MonoBehaviour
    {
        public PlayerState MeleePlayerMode = PlayerState.Idle;
        public float checkRadius;
        public LayerMask checkLayers;
        public Animator anim;
        public Transform enemy;
        public float health;
        public float armor;
        public float attack;
        public bool lockObj = false;
        public int count;

        public void Update()
        { 
            count = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (!lockObj || !Ready.Instance.isReady)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                Array.Sort(colliders, new DistanceCompare(transform));
                foreach (var item in colliders)
                {
                    enemy = item.transform;
                    lockObj = true;
                    break;
                }

                if (colliders.Length == 0)
                    lockObj = false;
            }
            
            if (enemy != null && GameManager.Instance.isStarted)
                UpdatePlayer(enemy);
        }

        public enum PlayerState
        {
            Idle,
            Attack,
            Die,
            MoveTowardsEnemy,
            Lock
        }
        
        public virtual void UpdatePlayer(Transform enemySol)
        {

        }
        
        protected void DoAction(Transform enemySol, PlayerState playerMode)
        {
            switch (playerMode)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.Attack:
                    Attack(enemySol);
                    break;
                case PlayerState.Die:
                    break;
                case PlayerState.MoveTowardsEnemy:
                    MoveTowards(enemySol);
                    break;
                case PlayerState.Lock:
                    break;
            }
        }

        private void MoveTowards(Transform enemySol)
        {
            transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
            transform.position = Vector3.Lerp(transform.position, enemySol.position, Time.deltaTime);
        }

        private void Attack(Transform enemySol)
        {
            if (count == 0)
            {
                MeleePlayerMode = PlayerState.Idle;
            }
            // transform.rotation = transform.LookAt(enemySol.position - transform.position);
            transform.LookAt(enemySol.position);
            float hit = attack - enemy.GetComponent<Enemy>().armor;
            if (enemy.GetComponent<MeleeEnemy>() != null)
                enemy.GetComponent<MeleeEnemy>().health -= hit * Time.deltaTime;
            if (enemy.GetComponent<RangeEnemy>() != null)
                enemy.GetComponent<RangeEnemy>().health -= hit * Time.deltaTime;
            enemy.GetComponent<Enemy>().health -= hit * Time.deltaTime;
        }
    }
}

