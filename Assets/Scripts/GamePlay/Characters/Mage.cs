using UnityEngine;
using DG.Tweening;

namespace KeepItAlive
{
    public class Mage : MonoBehaviour, ITakeDamage, IHeal
    {
        #region Public Variable

        public CharacterBehaviour MageBehaviour;
        public CharacterStats MageStats;
        public CharacterDataContainer MageData;

        public ObjectPooler ObjectPooler;

        public GameObject MagicBarrier;

        public Transform CastSpellPoint;

        public float ArcaneExlosionCooldown = 5f, AttackRate = 5f, WaterMagicCooldown = 2f;
        public GameCanvasUI GameLose;
        public HealthBar HealthBar;

        public ArcaneMagic arcaneMagic;

        public bool IsWaterMageReady;

        #endregion

        #region Private Variables

        private int _currentHealth;
        private int _maxHealth;

        private float _theScale;
        private float _xDirection;
        private float _nextArcaneExlosion = 0f;
        private float _nextAttackTime = 0f;
        private float _nextWaterAttackTIme = 0f;

        private bool canCast = false;

        #endregion

        #region Private Functions
        private void Start()
        {
            _maxHealth = MageStats.MaxHealth;
            _currentHealth = MageStats.MaxHealth;
            HealthBar.SetMaxHealth(MageStats.MaxHealth);
            ArcaneExplosionCooldown(ArcaneExlosionCooldown, MagicBarrier);
        }

        private void Update()
        {
            AnimationUpdate();


            _xDirection = Input.GetAxisRaw("Horizontal");

            if(_nextArcaneExlosion != ArcaneExlosionCooldown)
            {
                _nextArcaneExlosion += Time.deltaTime;
                if(_nextArcaneExlosion >= ArcaneExlosionCooldown)
                {
                    _nextArcaneExlosion = ArcaneExlosionCooldown;
                }
                canCast = false;
            }
            else
            {
                canCast = true;
            }

            if(Time.time >= _nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Attack();
                    _nextAttackTime = Time.time + 1f / AttackRate;
                }
            }

            if(canCast == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ArcaneExplosion(MagicBarrier, 1f);
                    _nextArcaneExlosion = 0f;
                }
            }


            if (_nextWaterAttackTIme >= WaterMagicCooldown)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    WaterMagic();
                    _nextWaterAttackTIme = 0f;
                     
                }

            }
            else
            {
                _nextWaterAttackTIme += Time.deltaTime;
                
            }

        }

        private void FixedUpdate()
        {
            MageBehaviour.Move(_xDirection, MageStats.Speed);
        }

        private void AnimationUpdate()
        {
            MageStats.CharacterAnimator.SetFloat("xVelocity", Mathf.Abs(MageBehaviour.CharacterRigidbody.velocity.x));           
        }

        private void ArcaneExplosion(GameObject gameObject, float scale)
        {
            ScaleBigger(gameObject, scale, ArcaneExlosionCooldown);
        }

        private void ArcaneExplosionCooldown(float time, GameObject gameObject)
        {
            float scale = 0.13f;

            ScaleSmall(scale, gameObject, time);
        }

        private void ScaleBigger(GameObject gameObject, float scale, float time)
        {
            arcaneMagic._arcaneMagicCollider2D.enabled = true;

            gameObject.transform.DOScale(scale, 1f);
            gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 1.2f).OnComplete(() =>
            {
                ArcaneExplosionCooldown(time, gameObject);
            });
        }        

        private void ScaleSmall(float scale, GameObject gameObject, float time)
        {
            arcaneMagic._arcaneMagicCollider2D.enabled = false;

            gameObject.transform.DOScale(scale, time);
            gameObject.GetComponent<SpriteRenderer>().DOFade(1f, time);
        }

        private void WaterMagic()
        {
            MageStats.CharacterAnimator.SetTrigger("Attack");
            ObjectPooler.SpawnFromPool("WaterMage", CastSpellPoint.position, CastSpellPoint.rotation);
        }
        #endregion

        #region Public Functions
        public void Attack()
        {
            MageStats.CharacterAnimator.SetTrigger("Attack");
            ObjectPooler.SpawnFromPool("FireBall", CastSpellPoint.position, CastSpellPoint.rotation);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            HealthBar.SetHealth(_currentHealth);

            if(_currentHealth <= 0)
            {
                GameLose.Lose();
                MageData.SaveTime("BestMage", DataContainer.Instance.CurrentBestMageTime);
                gameObject.SetActive(false);
            }

        }

        public void Heal(int heal)
        {
            _currentHealth += heal;
            if(_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            HealthBar.SetHealth(_currentHealth);
        }

        #endregion
    }
}

