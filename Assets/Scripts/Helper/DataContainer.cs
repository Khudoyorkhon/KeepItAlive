using TMPro;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public static DataContainer Instance;

    public TextMeshProUGUI BestTimeMage, BestTimeHunter, BestTimeRogue;

    public float CurrentBestMageTime = 0.00f, CurrentBestHunterTime = 0.00f, CurrentBestRogueTime= 0.00f;


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

        ShowData(BestTimeMage, "BestMage");        
        ShowData(BestTimeHunter, "BestHunter");        
        ShowData(BestTimeRogue, "BestRogue");

        CurrentBestMageTime = GetData("BestMage");
        CurrentBestHunterTime = GetData("BestHunter");
        CurrentBestRogueTime = GetData("BestRogue");

    }

    public void SetData(float time, string key)
    {
        PlayerPrefs.SetFloat(key, time);
    }

    public float GetData(string key)
    {
        return PlayerPrefs.GetFloat(key,0.00f);
    }



    private void ShowData(TextMeshProUGUI text, string key)
    {
        float tempVar = 0;

        if (text != null)
        {
            if (!PlayerPrefs.HasKey(key))
            {

                text.text = "0.00";
            }
            else
            {
                tempVar = GetData(key);
                text.text = tempVar.ToString("F2");
            }
        }
    }
}
