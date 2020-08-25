using System.Threading;
using UnityEngine;

namespace KeepItAlive
{
    public class Rogue : MonoBehaviour,IAttack, ITakeDamage, IHeal
    {
        #region Public Variable
        public Character RogueCharacter;

        public Transform AttackPoint;
        public Transform WallCheck;

        public float AttackRange = 0.5f;
        public float AttackRate = 1f;
        public float JumpTimeCounter = 0.35f;
        public float DashTime;
        public float DashSpeed;

        public HealthBar HealthBar;

        public Timer Timer;

        public GameCanvasUI gameCanvas;

        public LayerMask EnemyLayers;
        #endregion

        #region Private Varibale
        private float _xDirection;
        private float _nextAttackTime;
        private float _jumpTimeCounter;
        private float _dashTimeLeft;

        private bool _jump = false;

        private int _currentHealth;

        #endregion

        private void Start()
        {
            _currentHealth = RogueCharacter.MaxHealth;
            HealthBar.SetMaxHealth(RogueCharacter.MaxHealth);
        }

        private void Update()
        {
            AnimationUpdate();

            RogueCharacter.IsGrounded();

            _xDirection = Input.GetAxisRaw("Horizontal");


            if (Time.time >= _nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Attack(Random.Range(RogueCharacter.BaseDamage - RogueCharacter.DamageVariance, RogueCharacter.BaseDamage + RogueCharacter.DamageVariance));
                    _nextAttackTime = Time.time + 1f / AttackRate;
                }
            }

            if(Input.GetButtonDown("Jump") && RogueCharacter.IsGrounded() == true)
            {
 
                _jump = true;
                _jumpTimeCounter = JumpTimeCounter;
                RogueCharacter.Jump(RogueCharacter.JumpForce);
            }

            if (Input.GetKey(KeyCode.Space) && _jump == true)
            {

                if (_jumpTimeCounter > 0)
                {
                    RogueCharacter.Jump(RogueCharacter.JumpForce);
                    _jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    _jump = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _jump = false;
            }
        }

        private void FixedUpdate()
        {
            RogueCharacter.Move(_xDirection, RogueCharacter.Speed);
        }

        #region Public Function

        public void Attack(int damage)
        {
            RogueCharacter.CharacterAnimator.SetTrigger("Attack");


            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

            foreach(Collider2D detectedEnemy in hitEnemy)
            {
                detectedEnemy.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage);
                print(detectedEnemy.name);
                    takeDamage?.TakeDamage(damage);
            }

        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            HealthBar.SetHealth(_currentHealth);

            RogueCharacter.CharacterAnimator.SetTrigger("Hurt");

            if (_currentHealth <= 0)
            {
                RogueCharacter.CharacterAnimator.SetBool("isDead", true);
               // RogueCharacter.SaveTime();
                gameCanvas.Lose();

                gameObject.SetActive(false);
            }

        }

        public void Heal(int heal)
        {
            _currentHealth += heal;

            HealthBar.SetHealth(_currentHealth);

            if(_currentHealth >= RogueCharacter.MaxHealth)
            {
                _currentHealth = RogueCharacter.MaxHealth;
            }
        }

        #endregion
        #region Private Function


        private void AnimationUpdate()
        {
            RogueCharacter.CharacterAnimator.SetBool("isGounded", RogueCharacter.IsGrounded());
            RogueCharacter.CharacterAnimator.SetFloat("yVelocity", RogueCharacter.CharacterRigidbody.velocity.y);
            RogueCharacter.CharacterAnimator.SetFloat("Speed", Mathf.Abs(RogueCharacter.CharacterRigidbody.velocity.x));
        }

        private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
                return;

            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
        #endregion

    }
}

