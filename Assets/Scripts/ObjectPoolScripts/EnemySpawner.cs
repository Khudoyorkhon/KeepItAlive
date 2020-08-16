using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class EnemySpawner : MonoBehaviour
    {
        public ObjectPooler _objectPooler;

        public Transform SpawnPoint;

        public float NextEnemyTimer = 5f;

        private float _timer = 0f;

        private void Start()
        {
            _objectPooler.SpawnFromPool("Deamon", SpawnPoint.position, Quaternion.identity);
            _objectPooler.SpawnFromPool("Skeleton", SpawnPoint.position, Quaternion.identity);
        }

        private void FixedUpdate()
        {
            _timer += Time.deltaTime;

            if (_timer >= NextEnemyTimer)
            {
                _objectPooler.SpawnFromPool("Deamon", SpawnPoint.position, Quaternion.identity);
                _timer = 0f;
            }
            
        }
    }
}

