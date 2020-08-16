using TMPro;
using UnityEngine;


namespace KeepItAlive
{
    public class StopWatch : MonoBehaviour
    {
        #region Private Variable
        private float _timeStart = 0;
        #endregion

        #region Public Variable
        public TextMeshProUGUI StopWatchText;
        #endregion

        void Start()
        {
            StopWatchText.text = _timeStart.ToString("F2");
        }


        void Update()
        {
            _timeStart += Time.deltaTime;
            StopWatchText.text = _timeStart.ToString("F2");
        }
    }
}

