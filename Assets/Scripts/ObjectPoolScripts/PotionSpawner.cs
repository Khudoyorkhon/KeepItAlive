using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeepItAlive 
{
    public class PotionSpawner : MonoBehaviour
    {
        public Transform[] Positions;

        public ObjectPooler PotionPrefabSpawner;
        public float TimeToSpawnSet;

        private float _timeToSpawn = 0f;
        private Transform _newPosition;

        void Update()
        {
            _timeToSpawn += Time.deltaTime;
            if(_timeToSpawn >= TimeToSpawnSet)
            {    
                if(Random.value > .5f)
                {
                    _newPosition = Positions[Random.Range(0, Positions.Length)];
                    PotionPrefabSpawner.SpawnFromPool("Potion", _newPosition.position, _newPosition.rotation);
                }
                _timeToSpawn = 0f;
            }
        }
    }
}

