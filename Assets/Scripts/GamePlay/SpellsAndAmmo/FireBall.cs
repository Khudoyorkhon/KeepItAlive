using UnityEngine;


 namespace KeepItAlive
{
    public class FireBall : MonoBehaviour, IPooledObject
    {

        #region Public Variable
        public Projectile FireBallProjectile;
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
            FireBallProjectile.ProjectileRigidbody.velocity = _direction * FireBallProjectile.Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage))
                takeDamage?.TakeDamage(Random.Range(FireBallProjectile.Damage - FireBallProjectile.DamageVariance, FireBallProjectile.Damage + FireBallProjectile.DamageVariance));
            gameObject.SetActive(false);
        }
    }
}

