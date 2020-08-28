using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeepItAlive
{
    public class Deamon : MonoBehaviour, ITakeDamage, IPooledObject
    {
        public Enemy DeamonEnemy;

        private int _currentHealth;

        public void OnObjectSpawn()
        {
            _currentHealth = DeamonEnemy.Health;
        }        
        public void Start()
        {
            _currentHealth = DeamonEnemy.Health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            

            if (_currentHealth <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}

