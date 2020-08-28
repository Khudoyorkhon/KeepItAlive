using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace KeepItAlive
{
    public class WaterMageImageFade : MonoBehaviour
    {
        public Mage MageComponent;
        public Image Icon;

        private float _timeFade;
        private float _currentTime = 0f;

        private void Start()
        {
            _timeFade = MageComponent.WaterMagicCooldown;
            _currentTime = 0f;
        }


        private void FixedUpdate()
        {
            if(MageComponent.IsWaterMageReady == true)
            {
                _currentTime += Time.deltaTime;
                    if(_currentTime >= _timeFade)
                    {
                    _currentTime = _timeFade;
                    }
            }
            else
            {
                _currentTime = 0f;
            }

            Icon.fillAmount = _timeFade / _currentTime;
        }

    }

}


