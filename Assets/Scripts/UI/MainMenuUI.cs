using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject CharacterPanel, GameTitle;

    public void OpenAndCloseCharacterPanel()
    {
        if (CharacterPanel.activeInHierarchy)
        {
            CharacterPanel.SetActive(false);
            GameTitle.SetActive(true);
        }
        else
        {
            CharacterPanel.SetActive(true);
            GameTitle.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
