using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private AudioClip _restartGameSound; // DŸwiêk dla przycisku "Restart Game"
    [SerializeField] private AudioClip _exitGameSound;    // DŸwiêk dla przycisku "Exit Game"
    [SerializeField] private AudioSource _audioSource;    // AudioSource do odtwarzania dŸwiêków

    // Metoda do obs³ugi restartu gry
    public void RestartGame()
    {
        PlaySound(_restartGameSound); // Odtwórz dŸwiêk
        SceneManager.LoadScene("GameScene"); // Za³aduj scenê gry
    }

    // Metoda do obs³ugi zakoñczenia gry
    public void ExitGame()
    {
        PlaySound(_exitGameSound); // Odtwórz dŸwiêk
        Application.Quit();         // Zamknij aplikacjê
    }

    // Uniwersalna metoda do odtwarzania dŸwiêku
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(clip); // Odtwórz dŸwiêk jednokrotnie
        }
    }
}
