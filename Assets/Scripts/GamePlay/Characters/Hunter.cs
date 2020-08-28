using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeepItAlive
{
    public class Hunter : MonoBehaviour, ITakeDamage, IHeal
    {
        #region Public Variable
        public Character HunterCharacter;

        public ObjectPooler objectPooler;

        public HealthBar HealthBar;

        public Transform ShootPoint, CastSpellPoint;

        public GameCanvasUI GameCanvas;
        #endregion

        #region Private Variable

        private float _xDirection;
        private float _nextAttackTime;

        private int _currentHealth;

        private bool _canMove = false;


        #endregion
        private void Start()
        {
            _currentHealth = HunterCharacter.MaxHealth;
            print(_currentHealth);
            HealthBar.SetMaxHealth(HunterCharacter.MaxHealth);
            _canMove = true;
        }

        private void Update()
        {
            AnimationUpdate();
            HunterCharacter.IsGrounded();

            if(HunterCharacter.CanMove() == true && HunterCharacter.IsGrounded() == true && _canMove == true)
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

            if (Input.GetButtonDown("Jump") && HunterCharacter.IsGrounded() == true)
            {
                HunterCharacter.Jump(HunterCharacter.JumpForce);
            }
        }

        private void FixedUpdate()
        {
            HunterCharacter.Move(_xDirection, HunterCharacter.Speed);
        }


        private void AnimationUpdate()
        {
            HunterCharacter.CharacterAnimator.SetBool("isGrounded", HunterCharacter.IsGrounded());
            HunterCharacter.CharacterAnimator.SetFloat("yVelocity", HunterCharacter.CharacterRigidbody.velocity.y);
            HunterCharacter.CharacterAnimator.SetFloat("xVelocity", Mathf.Abs(HunterCharacter.CharacterRigidbody.velocity.x));
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            print(_currentHealth);

            HealthBar.SetHealth(_currentHealth);
            if (_currentHealth <= 0)
            {
                GameCanvas.Lose();
                HunterCharacter.SaveTime("BestHunter", DataContainer.Instance.CurrentBestHunterTime);
                gameObject.SetActive(false);
            }

        }

        public void Attack()
        {
            HunterCharacter.CharacterAnimator.SetTrigger("Shoot");

            objectPooler.SpawnFromPool("Bullet", ShootPoint.position, ShootPoint.rotation);
        }

        public void Heal(int heal)
        {
            _currentHealth += heal;
            print(_currentHealth);
            if (_currentHealth >= HunterCharacter.MaxHealth)
            {
                _currentHealth = HunterCharacter.MaxHealth;
            }

            HealthBar.SetHealth(_currentHealth);
        }

        public void CastSpell()
        {
            HunterCharacter.CharacterAnimator.SetTrigger("Shoot");

            objectPooler.SpawnFromPool("Nightmare", CastSpellPoint.position, CastSpellPoint.rotation);
        }


    }
}


