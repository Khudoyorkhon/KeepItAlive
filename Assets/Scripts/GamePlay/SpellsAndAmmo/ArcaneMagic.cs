using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class ArcaneMagic : MonoBehaviour
    {
        public Mage mage;
        public CircleCollider2D _arcaneMagicCollider2D = null;

        [SerializeField] private int _damage = 200;
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage))
                takeDamage?.TakeDamage(_damage);
        }

    }
}

