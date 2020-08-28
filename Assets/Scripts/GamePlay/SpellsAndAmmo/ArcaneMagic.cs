using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class ArcaneMagic : MonoBehaviour
    {
        public EdgeCollider2D _arcaneMagicCollider2D = null;

        private int _damage = 200;
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage))         
                takeDamage?.TakeDamage(_damage);
        }

    }
}

