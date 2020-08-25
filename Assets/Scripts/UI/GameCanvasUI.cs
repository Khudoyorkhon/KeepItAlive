using UnityEngine;

namespace KeepItAlive
{
    public class GameCanvasUI : MonoBehaviour
    {
        #region Public Varibable
        public GameObject Time, HealthBar, LosePanel;
        #endregion

        public void Lose()
        {
            ObjectActivationAndDeactivation(Time);
            ObjectActivationAndDeactivation(HealthBar);
            ObjectActivationAndDeactivation(LosePanel);
        }

        private void ObjectActivationAndDeactivation(GameObject gameObject)
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

    }
}

