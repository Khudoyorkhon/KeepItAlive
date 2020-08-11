using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class Nightmare : MonoBehaviour, IPooledObject
    {
        #region Public Variable

        public Projectile NightmareProjectile;

        #endregion

        #region Private Variable
        private Vector2 _nightmareDirection;
        #endregion

        public void OnObjectSpawn()
        {
            _nightmareDirection = transform.right;
        }

        private void FixedUpdate()
        {
            NightmareProjectile.ProjectileRigidbody.velocity = _nightmareDirection * NightmareProjectile.Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage))
                takeDamage?.TakeDamage(Random.Range(NightmareProjectile.Damage - NightmareProjectile.DamageVariance, NightmareProjectile.Damage + NightmareProjectile.DamageVariance));
            StartCoroutine(DeactivateObject());
        }

        IEnumerator DeactivateObject()
        {
            yield return new WaitForSeconds(0.5f);
            this.gameObject.SetActive(false);
        }

    }
}


