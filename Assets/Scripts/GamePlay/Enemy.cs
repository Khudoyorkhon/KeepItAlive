using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class Enemy : MonoBehaviour, ITakeDamage
    {

        public void TakeDamage(int damage)
        {
            print(damage);     
        }
    }
}


