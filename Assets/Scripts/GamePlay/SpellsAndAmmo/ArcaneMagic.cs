using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMagic : MonoBehaviour
{
    [SerializeField] private int _damage = 200;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ITakeDamage>(out ITakeDamage takeDamage))
            takeDamage?.TakeDamage(_damage);
    }

}
