using UnityEngine;

namespace KeepItAlive
{
    public class CharacterStats : MonoBehaviour
    {
        #region private Variable

        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _baseDamage = 25;
        [SerializeField] private int _damageVariance = 6;


        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _jumpForce = 400f;

        [SerializeField] private Animator _characterAnimation = null;

        #endregion

        #region public Varible

        public int DamageVariance => _damageVariance;
        public int MaxHealth => _maxHealth;
        public int BaseDamage => _baseDamage;
        public float Speed => _speed;
        public float JumpForce => _jumpForce;
        public Animator CharacterAnimator => _characterAnimation;

        #endregion
    }
}

