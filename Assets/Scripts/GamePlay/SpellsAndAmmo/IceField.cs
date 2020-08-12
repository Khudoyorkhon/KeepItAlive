using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class IceField : MonoBehaviour
    {
        [SerializeField] private float _freezTime;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy?.Stop();
                StartCoroutine(Timer());
                enemy?.Move();
            }                
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_freezTime);
        }
    }
}

