using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private AudioClip _restartGameSound; // D�wi�k dla przycisku "Restart Game"
    [SerializeField] private AudioClip _exitGameSound;    // D�wi�k dla przycisku "Exit Game"
    [SerializeField] private AudioSource _audioSource;    // AudioSource do odtwarzania d�wi�k�w

    // Metoda do obs�ugi restartu gry
    public void RestartGame()
    {
        PlaySound(_restartGameSound); // Odtw�rz d�wi�k
        SceneManager.LoadScene("GameScene"); // Za�aduj scen� gry
    }

    // Metoda do obs�ugi zako�czenia gry
    public void ExitGame()
    {
        PlaySound(_exitGameSound); // Odtw�rz d�wi�k
        Application.Quit();         // Zamknij aplikacj�
    }

    // Uniwersalna metoda do odtwarzania d�wi�ku
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(clip); // Odtw�rz d�wi�k jednokrotnie
        }
    }
}
