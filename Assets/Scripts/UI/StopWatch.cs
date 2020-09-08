using TMPro;
using UnityEngine;


namespace KeepItAlive
{
    public class StopWatch : MonoBehaviour
    {
        #region Private Variable
        private float _timeChanger = 0.02f;
        #endregion

        #region Public Variable
        public TextMeshProUGUI StopWatchText;
        public float TimeStart = 0;
        #endregion

        void Start()
        {
            StopWatchText.text = TimeStart.ToString("F2");
        }


        void Update()
        {
            TimeStart += _timeChanger;
            StopWatchText.text = TimeStart.ToString("F2");
        }
    }
}

