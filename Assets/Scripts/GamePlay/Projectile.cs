using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeepItAlive
{
    public class Projectile : MonoBehaviour
    {
        #region Private Variable

        [SerializeField] private float _speed = 13f;

        [SerializeField] private int _damage = 20;
        [SerializeField] private int _damageVariance = 6;

        [SerializeField] Rigidbody2D _projectileRigidbody = null;
        #endregion

        #region Public Variable
        public float Speed => _speed;

        public int Damage => _damage;
        public int DamageVariance => _damageVariance;

        public Rigidbody2D ProjectileRigidbody => _projectileRigidbody;
        #endregion

    }
}

