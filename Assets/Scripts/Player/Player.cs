using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StatePattern
{
    public class Player : MonoBehaviour
    {
        public float checkRadius;
        public LayerMask checkLayers;
        public Transform enemy;
        public float health;
        public float armor;
        public float attack;
        public bool lockObj = false;
     
        private void Update()
        {
            if (!lockObj || !Ready.Instance.isReady)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
                Array.Sort(colliders, new DistanceCompare(transform));
                foreach (var item in colliders)
                {
                    enemy = item.transform;
                    lockObj = true;
                    //Ready.instance.isReady = true;
                    break;
                }   
                
                if (colliders.Length == 0)
                    enemy = null;
            }
            if (enemy == null)
            {
                lockObj = false;
            }
            if (enemy != null && GameManager.Instance.isStarted)
            {
                UpdatePlayer(enemy);
            }
            float attackSpeed = 5f;
        }

        protected enum PlayerState
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
                    transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
                    if(enemy.GetComponent<MeleeEnemy>() != null)
                        enemy.GetComponent<MeleeEnemy>().health -= 30 * Time.deltaTime;
                    if(enemy.GetComponent<RangeEnemy>() != null)
                        enemy.GetComponent<RangeEnemy>().health -= 30 * Time.deltaTime;
                    enemy.GetComponent<Enemy>().health -= 30 * Time.deltaTime;
                    break;
                case PlayerState.Die:
                    break;
                case PlayerState.MoveTowardsEnemy:
                    transform.rotation = Quaternion.LookRotation(enemySol.position - transform.position);
                    transform.position = Vector3.Lerp(transform.position, enemySol.position, Time.deltaTime);
                    break;
                case PlayerState.Lock:
                    break;
            }
        }
    }
}

