using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class Cube : MonoBehaviour, IPooledObject
    {
        #region public Variable
        public float upForce = 1f;
        public float sideForce = 0.1f;
        #endregion

        public void OnObjectSpawn()
        {
            float xForce = Random.Range(-sideForce, sideForce);            
            float yForce = Random.Range(-upForce * 0.5f, upForce);
            float zForce = Random.Range(-sideForce, sideForce);

            Vector3 force = new Vector3(xForce, yForce, zForce);

            GetComponent<Rigidbody>().velocity = force;

        }
    }
}


