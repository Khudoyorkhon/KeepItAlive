using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class WaterWave : MonoBehaviour, IPooledObject
    {

        #region Public Variable
        public Projectile WaterWaveProjectile;
        #endregion

        #region Private Variable
        private Vector3 _direction;

        public void OnObjectSpawn()
        {
            _direction = transform.right;
        }
        #endregion


        // Update is called once per frame
        void Update()
        {

        }
    }

}
