using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class Rogue : MonoBehaviour,IAttack, ITakeDamage
    {
        #region Public Variable

        public Character RogueController;

        public Transform AttackPoint;
        public Transform WallCheck;

        public float AttackRange = 0.5f;
        public float AttackRate = 1f;
        public float WallCheckRadius = 0.2f;

        public float SideForce;

        public LayerMask EnemyLayers;
        public LayerMask WallLayer;
        #endregion

        #region Private Varibale
        private float _xDirection;
        private float _nextAttackTime;

        private bool _jump = false;
        private bool _isWall = false;
        private bool _isFacingRigtht = true;
        private bool _isOnRigthWall = false;

        private int _currentHealth;
        private int _wallJumpDirection = -1;


        public Vector2 WallJumpDirection;
        #endregion

        private void Start()
        {
            _currentHealth = RogueController.MaxHealth;
        }

        void Update()
        {
            _isWall = Physics2D.OverlapCircle(WallCheck.position, WallCheckRadius, WallLayer);
            print(_isWall);

            _isOnRigthWall = _isWall;

            RogueController.IsGrounded();

            if(_isWall == true && Input.GetButtonDown("Jump") && RogueController.IsGrounded() == false)
            {
                WallJump();
            }

            if(RogueController.CanMove() == true && RogueController.IsGrounded() == true)
            {
                _xDirection = Input.GetAxisRaw("Horizontal");
            }

            if(Time.time >= _nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Attack(Random.Range(RogueController.BaseDamage - RogueController.DamageVariance, RogueController.BaseDamage + RogueController.DamageVariance));
                    _nextAttackTime = Time.time + 1f / AttackRate;
                }
            }



            if(Input.GetButtonDown("Jump") && RogueController.IsGrounded() == true && _isWall == false)
            {
                _jump = true;
                RogueController.CharacterAnimator.SetBool("Jump", _jump);
                RogueController.Jump(RogueController.JumpForce);
            }
            else
            {
                _jump = false;
                RogueController.CharacterAnimator.SetBool("Jump", _jump);
            }
        }

        private void FixedUpdate()
        {
            RogueController.Move(_xDirection, RogueController.Speed);

        }

        public void WallJump()
        {
            if (_isOnRigthWall)
            {
                
                _wallJumpDirection = -1;
                RogueController.CharacterRigidbody.velocity = new Vector2(-1 * _xDirection * SideForce, RogueController.JumpForce);
                
            }
            
        }

        public void Attack(int damage)
        {
            RogueController.CharacterAnimator.SetTrigger("Attack");


            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

            foreach(Collider2D detectedEnemy in hitEnemy)
            {
                detectedEnemy.TryGetComponent<Enemy>(out Enemy enemy);
                    enemy?.TakeDamage(damage);
            }

        }

        private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
                return;

            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
            Gizmos.DrawWireSphere(WallCheck.position, WallCheckRadius);
        }

        public void TakeDamage(int damage)
        {

            _currentHealth -= damage;

            RogueController.CharacterAnimator.SetTrigger("Hurt");

            if (_currentHealth <= 0)
            {
                RogueController.CharacterAnimator.SetBool("isDie", true);
            }

        }

    }
}

