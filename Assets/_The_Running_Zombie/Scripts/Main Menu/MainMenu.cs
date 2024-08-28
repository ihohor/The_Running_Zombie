using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _playGameSound; // DŸwiêk dla przycisku "Play Game"
    [SerializeField] private AudioClip _exitGameSound; // DŸwiêk dla przycisku "Exit Game"
    [SerializeField] private AudioSource _audioSource; // AudioSource do odtwarzania dŸwiêków

    // Metoda do odtwarzania dŸwiêku przycisku "Play Game"
    public void PlayGame()
    {
        PlaySound(_playGameSound); // Odtwórz dŸwiêk
        SceneManager.LoadScene("CutScene");
    }

    // Metoda do odtwarzania dŸwiêku przycisku "Exit Game"
    public void ExitGame()
    {
        PlaySound(_exitGameSound); // Odtwórz dŸwiêk
        Application.Quit();
    }

    // Uniwersalna metoda do odtwarzania dŸwiêku
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
