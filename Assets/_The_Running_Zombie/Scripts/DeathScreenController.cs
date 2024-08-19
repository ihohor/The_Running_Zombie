using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Zmieñ nazwê na nazwê g³ównej sceny gry
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
