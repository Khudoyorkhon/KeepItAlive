using UnityEngine.UI;
using UnityEngine;

namespace KeepItAlive
{

    public class DamageText : MonoBehaviour
    {
        #region Private Varible

        [SerializeField] private Text _text = null;
        [SerializeField] private string _template = null;

        #endregion

        #region Public Function

        public void SetDamage(int damage)
        {
            _text.CrossFadeAlpha(1,0.1f,false);

            _text.text = string.Format(_template, damage);

            _text.CrossFadeAlpha(0, 1f, false);
        }

        #endregion
    }

}

