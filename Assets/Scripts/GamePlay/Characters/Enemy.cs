using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

namespace KeepItAlive
{
    public class Enemy : MonoBehaviour, ITakeDamage
    {
        #region Private Variable
        [SerializeField] private Transform Target = null;

        [SerializeField] private float Speed = 20f;
        [SerializeField] private float nextWayPointDistance = 3f;

        [SerializeField] private Path path;

        [SerializeField] private int currentWayPoint = 0;

        [SerializeField] private bool _reachEndPoint = false;
        [SerializeField] private bool _flyEnemy = false;
        [SerializeField] private bool _groundEnemy = false;
                    

        [SerializeField] private Seeker seeker = null;
        [SerializeField] private Rigidbody2D _enemyRigidbody = null;

        private float _distance = 0f;

        private Vector2 _direction;

        private bool canMove = true;

        public Rigidbody2D EnemyRigidbody { get => _enemyRigidbody; set => _enemyRigidbody = value; }
        #endregion
        private void Start()
        {
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }

        private void UpdatePath()
        {
            if(seeker.IsDone())
                seeker.StartPath(_enemyRigidbody.position, Target.position, OnCompletePath);
        }

        private void OnCompletePath(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWayPoint = 0;
            }
        }
        private void FixedUpdate()
        {
            if(canMove == true)
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

            }
        }


        public void TakeDamage(int damage)
        {
            print(damage);
        }
        public void Stop()
        {
            canMove = false;
        }

        public void Move()
        {
            canMove = true;
        }

    }

}


