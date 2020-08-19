using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject CharacterPanel;

    public void OpenAndCloseCharacterPanel()
    {
        if (CharacterPanel.activeInHierarchy)
        {
            CharacterPanel.SetActive(false);
        }
        else
        {
            CharacterPanel.SetActive(true);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
