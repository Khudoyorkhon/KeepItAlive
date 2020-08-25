using KeepItAlive;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LosePanel : MonoBehaviour
{
    public TextMeshProUGUI LevelTime;
    public StopWatch StopWatch;
    void Start()
    {
        LevelTime.text = "Your Time: " + StopWatch.TimeStart.ToString("F2");
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
