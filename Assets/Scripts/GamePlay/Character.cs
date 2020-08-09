using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class Character : MonoBehaviour
    {
        #region private Variable

        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _baseDamage = 25;
        [SerializeField] private int _damageVariance = 6;


        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private float _jumpForce = 400f;

        [SerializeField] private Rigidbody2D _characterRigidbody=null;



        [SerializeField] private Animator _characterAnimation = null;

        [SerializeField] private Transform _groundCheck = null;

        [SerializeField] private LayerMask _whatIsGround;

        private Vector3 theScale;

        private bool _isGrounded;
        private bool _canMove;
        private bool _isFacingRigtht = true;

        #endregion

        #region public Varible

        public int DamageVariance => _damageVariance;
        public int MaxHealth => _maxHealth;
        public int BaseDamage => _baseDamage;
        public float Speed => _speed;
        public float JumpForce => _jumpForce;

        public Rigidbody2D CharacterRigidbody => _characterRigidbody;

        public Animator CharacterAnimator => _characterAnimation;



        #endregion

        #region Public Functions

        public bool IsGrounded()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);

            return _isGrounded;
        }

        public bool CanMove()
        {
            if (_isGrounded)
            {
                _canMove = true;
            }
            else
            {
                _canMove = false;
            }

            return _canMove;
        }

        public void Jump(float jumpForce)
        {
            _characterRigidbody.velocity = new Vector2(_characterRigidbody.velocity.x, jumpForce);
        }

        public void Move(float direction, float speed)
        {
            _characterRigidbody.velocity = new Vector2(direction * speed, _characterRigidbody.velocity.y);

            if(direction < 0 && _isFacingRigtht)
            {
                FlipCharacter();
            }
            else if(direction >0 && !_isFacingRigtht)
            {
                FlipCharacter();
            }
            _characterAnimation.SetFloat("Speed", Mathf.Abs(_characterRigidbody.velocity.x));
        }

        public void FlipCharacter()
        {
            _isFacingRigtht = !_isFacingRigtht;

            theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

        }

        #endregion

        #region Private Functions


        #endregion

    }
}

