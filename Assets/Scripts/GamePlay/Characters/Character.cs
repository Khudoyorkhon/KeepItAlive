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

        private bool _isGrounded;
        private bool _canMove;
        private bool _isFacingRigtht = true;

        private int _currentHealth;
        #endregion

        #region public Varible

        public int DamageVariance => _damageVariance;
        public int MaxHealth => _maxHealth;
        public int BaseDamage => _baseDamage;
        public float Speed => _speed;
        public float JumpForce => _jumpForce;

        public Rigidbody2D CharacterRigidbody => _characterRigidbody;

        public StopWatch Timer;
        public Animator CharacterAnimator => _characterAnimation;

        #endregion

        #region Public Functions

        private void Start()
        {
            _currentHealth = MaxHealth;
        }

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
            
        }

        public void FlipCharacter()
        {
            _isFacingRigtht = !_isFacingRigtht;

            transform.Rotate(0.0f, 180.0f, 0.0f);

        }

        public void SaveTime()
        {
            if (Timer.TimeStart > DataContainer.Instance.CurrentBestTime)
            {
                DataContainer.Instance.SetData(Timer.TimeStart, "BestTime");
            }
        }

        public void Jump(float jumpForce)
        {
            _characterRigidbody.velocity = Vector2.up * jumpForce;
        }
        #endregion


    }
}

