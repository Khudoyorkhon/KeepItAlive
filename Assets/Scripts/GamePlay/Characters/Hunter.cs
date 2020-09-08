using UnityEngine;


namespace KeepItAlive
{
    public class Hunter : MonoBehaviour, ITakeDamage, IHeal
    {
        #region Public Variable

        public CharacterBehaviour HunterBehaviour;
        public CharacterStats HunterStats;
        public CharacterDataContainer HunterData;

        public ObjectPooler objectPooler;

        public HealthBar HealthBar;

        public Transform ShootPoint, CastSpellPoint;

        public GameCanvasUI GameCanvas;

        #endregion

        #region Private Variable

        private float _xDirection;

        private int _currentHealth;
        private int _maxHealth;

        private bool _canMove = false;

        #endregion

        #region Private Functions
        private void Start()
        {
            _maxHealth = HunterStats.MaxHealth;
            _currentHealth = HunterStats.MaxHealth;

            HealthBar.SetMaxHealth(_maxHealth);

            _canMove = true;
        }

        private void Update()
        {
            AnimationUpdate();
            HunterBehaviour.IsGrounded();

            if(HunterBehaviour.CanMove() == true && HunterBehaviour.IsGrounded() == true && _canMove == true)
            {
                _xDirection = Input.GetAxisRaw("Horizontal");
            }

            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                _canMove = false;
            }
            else
            {
                _canMove = true;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                CastSpell();
            }

            if (Input.GetButtonDown("Jump") && HunterBehaviour.IsGrounded() == true)
            {
                HunterBehaviour.Jump(HunterStats.JumpForce);
            }
        }

        private void FixedUpdate()
        {
            HunterBehaviour.Move(_xDirection, HunterStats.Speed);
        }


        private void AnimationUpdate()
        {
            HunterStats.CharacterAnimator.SetBool("isGrounded", HunterBehaviour.IsGrounded());
            HunterStats.CharacterAnimator.SetFloat("yVelocity", HunterBehaviour.CharacterRigidbody.velocity.y);
            HunterStats.CharacterAnimator.SetFloat("xVelocity", Mathf.Abs(HunterBehaviour.CharacterRigidbody.velocity.x));
        }

        private void CastSpell()
        {
            HunterStats.CharacterAnimator.SetTrigger("Shoot");

            objectPooler.SpawnFromPool("Nightmare", CastSpellPoint.position, CastSpellPoint.rotation);
        }
        private void Attack()
        {
            HunterStats.CharacterAnimator.SetTrigger("Shoot");

            objectPooler.SpawnFromPool("Bullet", ShootPoint.position, ShootPoint.rotation);
        }

        #endregion

        #region Public Functions
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            print(_currentHealth);

            HealthBar.SetHealth(_currentHealth);
            if (_currentHealth <= 0)
            {
                GameCanvas.Lose();
                HunterData.SaveTime("BestHunter", DataContainer.Instance.CurrentBestHunterTime);
                gameObject.SetActive(false);
            }

        }

        public void Heal(int heal)
        {
            _currentHealth += heal;
            print(_currentHealth);
            if (_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            HealthBar.SetHealth(_currentHealth);
        }

        #endregion
    }
}


