using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Zmie� nazw� na nazw� g��wnej sceny gry
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
