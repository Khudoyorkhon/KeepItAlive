using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

namespace KeepItAlive
{
    public class Enemy : MonoBehaviour, ITakeDamage
    {
        public Transform Target;

        public float Speed = 20f;
        public float nextWayPointDistance = 3f;

        private Path path;
        private int currentWayPoint = 0;
        private bool reachEndPoint = false;

        [SerializeField] private Seeker seeker = null;
        [SerializeField] private Rigidbody2D _enemyRigidbody = null;

        private float _distance = 0f;

        private Vector2 _direction;

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
            if (path == null)
                return;

            if(currentWayPoint >= path.vectorPath.Count)
            {
                reachEndPoint = true;
                return;
            }
            else
            {
                reachEndPoint = false;
            }

            _direction = ((Vector2)path.vectorPath[currentWayPoint] - _enemyRigidbody.position).normalized;

            _enemyRigidbody.velocity = _direction * Speed * Time.deltaTime;

            _distance = Vector2.Distance(_enemyRigidbody.position, path.vectorPath[currentWayPoint]);

            if(_distance < nextWayPointDistance)
            {
                currentWayPoint++;
            }

        }
        public void TakeDamage(int damage)
        {
            print(damage);     
        }
    }
}


