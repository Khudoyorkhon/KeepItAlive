using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

namespace KeepItAlive
{
    public class Enemy : MonoBehaviour, ITakeDamage, IAttack
    {
        #region Private Variable

        [SerializeField] private float Speed = 20f;
        [SerializeField] private float nextWayPointDistance = 3f;

        [SerializeField] private Path path;

        [SerializeField] private int currentWayPoint = 0;

        private bool _reachEndPoint = false;
        [SerializeField] private bool _flyEnemy = false;
        [SerializeField] private bool _groundEnemy = false;
                    

        [SerializeField] private Seeker seeker = null;
        [SerializeField] private Rigidbody2D _enemyRigidbody = null;

        private float _distance = 0f;

        private float _distanceToTarget = 0;

        private Vector2 _direction;

        #endregion

        #region Public Varibale
        public Transform Target = null;

        public float AttackDistance = 8f;
        public Rigidbody2D EnemyRigidbody { get => _enemyRigidbody; set => _enemyRigidbody = value; }

        public Animator EnemyAnimator;

        public int Damage;
        #endregion
        private void Start()
        {
            InvokeRepeating("UpdatePath", 0f, 0.5f);
            _distanceToTarget = transform.position.x;
        }

        private void UpdatePath()
        {
            if(seeker.IsDone())
                seeker.StartPath(_enemyRigidbody.position, Target.position, OnCompletePath);
        }

        private void FixedUpdate()
        {
            FindTarget();
        }

        private void OnCompletePath(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWayPoint = 0;
            }
        }
        public void FindTarget()
        {
            if (path == null)
                return;

            if (currentWayPoint >= path.vectorPath.Count)
            {
                _reachEndPoint = true;
                return;
            }
            else
            {
                _reachEndPoint = false;
            }

            _direction = ((Vector2)path.vectorPath[currentWayPoint] - _enemyRigidbody.position).normalized;

            if (_flyEnemy)
            {
                _enemyRigidbody.velocity = _direction * Speed * Time.deltaTime;
            }

            if (_groundEnemy)
            {
                _enemyRigidbody.velocity = new Vector2(_direction.x * Speed * Time.deltaTime, _enemyRigidbody.velocity.y);
            }


            _distance = Vector2.Distance(_enemyRigidbody.position, path.vectorPath[currentWayPoint]);

            if (_distance < nextWayPointDistance)
            {
                currentWayPoint++;
            }


            CheckDistance();

            if (_distanceToTarget < AttackDistance)
            {
                Attack(UnityEngine.Random.Range(Damage - 5, Damage + 5));
            }
            else
            {
                CheckDistance();
            }
        }

        private void CheckDistance()
        {
            _distanceToTarget = Vector2.Distance(Target.position, _enemyRigidbody.position);
            
        }

        public void TakeDamage(int damage)
        {
            print(damage);
        }

        public void Attack(int damage)
        {
            EnemyAnimator.SetTrigger("Attack");
        }
    }

}


