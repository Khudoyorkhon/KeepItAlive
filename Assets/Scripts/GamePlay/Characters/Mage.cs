using UnityEngine;
using DG.Tweening;

namespace KeepItAlive
{
    public class Mage : MonoBehaviour, ITakeDamage, IAttack
    {

        #region Public Variable
        public Character MageCharacter;

        public GameObject MagicBarrier;

        public Transform CastSpellPoint;

        public float ArcaneExlosionCooldown = 5f;


        public ArcaneMagic arcaneMagic;

        #endregion

        #region Private Variables
        private int _currentHealth;

        private float _theScale;

        private float _xDirection;

        private float _nextArcaneExlosion = 0f;

        private bool canCast = false;

        #endregion

        private void Start()
        {
            _currentHealth = MageCharacter.MaxHealth;

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

            if(canCast == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ArcaneExplosion(MagicBarrier, 1f);
                    _nextArcaneExlosion = 0f;
                }
            }

        }

        private void FixedUpdate()
        {
            MageCharacter.Move(_xDirection, MageCharacter.Speed);
        }

        private void AnimationUpdate()
        {
            
            MageCharacter.CharacterAnimator.SetFloat("xVelocity", Mathf.Abs(MageCharacter.CharacterRigidbody.velocity.x));           
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

        public void TakeDamage(int damage)
        {
            MageCharacter.CharacterAnimator.SetTrigger("TakeDamage");
            throw new System.NotImplementedException();
        }

        public void Attack(int damage)
        {
            MageCharacter.CharacterAnimator.SetTrigger("Attack");
            throw new System.NotImplementedException();
        }
    }
}

