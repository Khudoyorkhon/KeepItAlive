using UnityEngine;

namespace KeepItAlive
{
    public class CharacterBehaviour : MonoBehaviour
    {
        #region private Variable
        [SerializeField] private float _groundCheckRadius = 0.2f;

        [SerializeField] private Transform _groundCheck = null;

        [SerializeField] private Rigidbody2D _characterRigidbody = null;

        private bool _isGrounded;
        private bool _canMove;
        private bool _isFacingRigtht = true;
        #endregion

        #region Public Varibale
        public Rigidbody2D CharacterRigidbody => _characterRigidbody;

        public LayerMask WhatIsGround;

        #endregion

        public bool IsGrounded()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, WhatIsGround);

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

        public void Move(float direction, float speed)
        {
            _characterRigidbody.velocity = new Vector2(direction * speed, _characterRigidbody.velocity.y);

            if (direction < 0 && _isFacingRigtht)
            {
                FlipCharacter();
            }
            else if (direction > 0 && !_isFacingRigtht)
            {
                FlipCharacter();
            }

        }

        public void FlipCharacter()
        {
            _isFacingRigtht = !_isFacingRigtht;

            transform.Rotate(0.0f, 180.0f, 0.0f);

        }


        public void Jump(float jumpForce)
        {
            _characterRigidbody.velocity = Vector2.up * jumpForce;
        }

    }
}

