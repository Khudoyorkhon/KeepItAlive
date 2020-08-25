using TMPro;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public static DataContainer Instance;

    public TextMeshProUGUI BestTime = null;

    public float CurrentBestTime = 0.00f;


    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this; 
        }
        else if (Instance == this)
        { 
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

        if (BestTime != null)
        {
            if (!PlayerPrefs.HasKey("BestTime"))
            {

                BestTime.text = "0.00";
            }
            else
            {
                BestTime.text = GetData("BestTime").ToString("F2");
                CurrentBestTime = GetData("BestTime");
            }
        }

    }

    public void SetData(float time, string key)
    {
        PlayerPrefs.SetFloat(key, time);
    }

    public float GetData(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
}
