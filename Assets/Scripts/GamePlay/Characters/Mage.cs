using System.Collections;
using System.Collections.Generic;
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

        #endregion

        #region Private Variables
        private int _currentHealth;

        private Tween _tween;

        private Vector3 _theScale;

        private float _xDirection;

        private float _nextArcaneExlosion = 0f;

        private bool canCast = false;
        #endregion

        private void Start()
        {
            _currentHealth = MageCharacter.MaxHealth;
            StartCoroutine(ArcaneExplosionCooldown(ArcaneExlosionCooldown));
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
                    StartCoroutine(ArcaneExplosion());
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


        private IEnumerator ArcaneExplosion()
        {
            _theScale = new Vector3(1f, 1f, 1f);
            _tween.Kill();
            MagicBarrier.transform.DOScale(_theScale, 1f);
            MagicBarrier.GetComponent<SpriteRenderer>().DOFade(0f, 1.2f);

            yield return new WaitForSeconds(1.5f);

            yield return StartCoroutine(ArcaneExplosionCooldown(ArcaneExlosionCooldown));
        }

        private IEnumerator ArcaneExplosionCooldown(float time)
        {
            yield return new WaitForFixedUpdate();

            _theScale = new Vector3(0.13f, 0.13f, 0.13f);
            _tween.Kill();
            MagicBarrier.transform.DOScale(_theScale, time);
            MagicBarrier.GetComponent<SpriteRenderer>().DOFade(1f, time);
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

