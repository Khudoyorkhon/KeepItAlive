using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class EnemySpawner : MonoBehaviour
    {
        public ObjectPooler _objectPooler;

        public Transform[] SpawnPoint;

        public float NextDeamonTimer = 2f, NextSkeletonTimer = 1f, DeamonStartSpawnTime = 5f;

        private float _deamonTimer = 0f, _skeletTimer = 0f, _deamonStartSpawnTime = 0f;

        private void Start()
        {
            _objectPooler.SpawnFromPool("Deamon", SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.identity);
            _objectPooler.SpawnFromPool("Skeleton", SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.identity);
        }

        private void FixedUpdate()
        {
            SkeletonSpawn();
            DeamonSpawn();
        }

        private void SkeletonSpawn()
        {
            _skeletTimer += Time.deltaTime;

            if(_skeletTimer >= NextSkeletonTimer)
            {
                _objectPooler.SpawnFromPool("Skeleton", SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.identity);
                _skeletTimer = 0;
            }
           
        }

        private void DeamonSpawn()
        {
            if (_deamonStartSpawnTime <= DeamonStartSpawnTime)
            {
                _deamonStartSpawnTime += Time.deltaTime;
            }
            else
            {
                _deamonTimer += Time.deltaTime;

                if (_deamonTimer >= NextDeamonTimer)
                {
                    _objectPooler.SpawnFromPool("Deamon", SpawnPoint[Random.Range(0, SpawnPoint.Length)].position, Quaternion.identity);
                    _deamonTimer = 0f;
                }
            }
        }
    }
}

