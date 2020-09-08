using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

namespace KeepItAlive
{
    public class Skelet : MonoBehaviour, ITakeDamage,  IPooledObject
    {
        public Enemy SkeletEnemy;


        private int _currentHealth;

        public void OnObjectSpawn()
        {
            _currentHealth = SkeletEnemy.Health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            SkeletEnemy.EnemyAnimator.SetTrigger("TakeHit");



            if(_currentHealth <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class HitEvent : UnityEvent<int> { }
}



