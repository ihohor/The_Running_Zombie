using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CutScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
