using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class CubeSpawner : MonoBehaviour
    {
        private ObjectPooler _objectPooler;

        private void Start()
        {
            _objectPooler = ObjectPooler.Instance;
        }

        private void FixedUpdate()
        {
            _objectPooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);
        }
    }
}


