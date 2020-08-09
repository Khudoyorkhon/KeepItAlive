using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class Enemy : MonoBehaviour, ITakeDamage
    {

        public Character EnemyCharacter;

        private int _currentHealth;

        // Start is called before the first frame update
        void Start()
        {
            _currentHealth = EnemyCharacter.MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(int damage)
        {

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Debug.Log("Die");
            }

        }
    }
}


