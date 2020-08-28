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

        #endregion

        public void OnObjectSpawn()
        {
            _direction = transform.right;
        }
        // Update is called once per frame
        private void FixedUpdate()
        {
            WaterWaveProjectile.ProjectileRigidbody.velocity = _direction * WaterWaveProjectile.Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage))
                takeDamage?.TakeDamage(Random.Range(WaterWaveProjectile.Damage - WaterWaveProjectile.DamageVariance, WaterWaveProjectile.Damage + WaterWaveProjectile.DamageVariance));
            StartCoroutine(SetToFalse());
        }

        IEnumerator SetToFalse()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }

}
