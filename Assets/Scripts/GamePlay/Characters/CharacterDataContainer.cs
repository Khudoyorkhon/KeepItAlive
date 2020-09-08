using UnityEngine;

namespace KeepItAlive
{    public class CharacterDataContainer : MonoBehaviour
    {
        #region Public Variable
        public StopWatch Timer;
        #endregion

        public void SaveTime(string key, float currentBestTime)
        {
            if (Timer.TimeStart > currentBestTime)
            {
                DataContainer.Instance.SetData(Timer.TimeStart, key);
            }
        }
    }
}


