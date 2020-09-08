using UnityEngine;

namespace KeepItAlive
{
    public class Rogue : MonoBehaviour,IAttack, ITakeDamage, IHeal
    {
        #region Public Variable

        public CharacterBehaviour RogueBehaviour;
        public CharacterStats RogueStats;
        public CharacterDataContainer RogueData;

        public Transform AttackPoint;

        public float AttackRange = 0.5f;
        public float AttackRate = 1f;
        public float JumpTimeCounter = 0.35f;

        public HealthBar HealthBar;

        public GameCanvasUI GameCanvas;

        public LayerMask EnemyLayers;

        #endregion

        #region Private Varibale

        private float _xDirection;
        private float _nextAttackTime;
        private float _jumpTimeCounter;

        private bool _jump = false;

        private int _currentHealth;
        private int _maxHealth;

        #endregion

        #region Private Function

        private void Start()
        {
            _maxHealth = RogueStats.MaxHealth;
            _currentHealth = RogueStats.MaxHealth;
            HealthBar.SetMaxHealth(RogueStats.MaxHealth);
        }
        private void Update()
        {
            AnimationUpdate();

            RogueBehaviour.IsGrounded();

            _xDirection = Input.GetAxisRaw("Horizontal");


            if (Time.time >= _nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Attack(Random.Range(RogueStats.BaseDamage - RogueStats.DamageVariance, RogueStats.BaseDamage + RogueStats.DamageVariance));
                    _nextAttackTime = Time.time + 1f / AttackRate;
                }
            }

            if (Input.GetButtonDown("Jump") && RogueBehaviour.IsGrounded() == true)
            {

                _jump = true;
                _jumpTimeCounter = JumpTimeCounter;
                RogueBehaviour.Jump(RogueStats.JumpForce);
            }

            if (Input.GetKey(KeyCode.Space) && _jump == true)
            {

                if (_jumpTimeCounter > 0)
                {
                    RogueBehaviour.Jump(RogueStats.JumpForce);
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
            RogueBehaviour.Move(_xDirection, RogueStats.Speed);
        }
        private void AnimationUpdate()
        {
            RogueStats.CharacterAnimator.SetBool("isGounded", RogueBehaviour.IsGrounded());
            RogueStats.CharacterAnimator.SetFloat("yVelocity", RogueBehaviour.CharacterRigidbody.velocity.y);
            RogueStats.CharacterAnimator.SetFloat("Speed", Mathf.Abs(RogueBehaviour.CharacterRigidbody.velocity.x));
        }

        private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
                return;

            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }

        #endregion

        #region Public Function

        public void Attack(int damage)
        {
            RogueStats.CharacterAnimator.SetTrigger("Attack");


            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

            foreach(Collider2D detectedEnemy in hitEnemy)
            {
                detectedEnemy.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage);
                    takeDamage?.TakeDamage(damage);
            }

        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            HealthBar.SetHealth(_currentHealth);

            RogueStats.CharacterAnimator.SetTrigger("Hurt");

            if (_currentHealth <= 0)
            {
                RogueStats.CharacterAnimator.SetBool("isDead", true);
               
                GameCanvas.Lose();
                RogueData.SaveTime("BestRogue", DataContainer.Instance.CurrentBestRogueTime);
                gameObject.SetActive(false);
            }

        }

        public void Heal(int heal)
        {
            _currentHealth += heal;

            HealthBar.SetHealth(_currentHealth);

            if(_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }

        #endregion
    }
}

