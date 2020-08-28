using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class Bullet : MonoBehaviour, IPooledObject
    {
        #region Public Variable

        public Projectile BulletProjectile;

        #endregion

        #region Private Variable
        private Vector2 _bulletDirection;
        #endregion
        public void OnObjectSpawn()
        {
            _bulletDirection = transform.right;
        }

        private void FixedUpdate()
        {
            BulletProjectile.ProjectileRigidbody.velocity = _bulletDirection * BulletProjectile.Speed;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage))
                takeDamage?.TakeDamage(Random.Range(BulletProjectile.Damage - BulletProjectile.DamageVariance, BulletProjectile.Damage + BulletProjectile.DamageVariance));
            gameObject.SetActive(false);
        }


    }
}

