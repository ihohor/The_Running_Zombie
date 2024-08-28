using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _playGameSound; // D�wi�k dla przycisku "Play Game"
    [SerializeField] private AudioClip _exitGameSound; // D�wi�k dla przycisku "Exit Game"
    [SerializeField] private AudioSource _audioSource; // AudioSource do odtwarzania d�wi�k�w

    // Metoda do odtwarzania d�wi�ku przycisku "Play Game"
    public void PlayGame()
    {
        PlaySound(_playGameSound); // Odtw�rz d�wi�k
        SceneManager.LoadScene("CutScene");
    }

    // Metoda do odtwarzania d�wi�ku przycisku "Exit Game"
    public void ExitGame()
    {
        PlaySound(_exitGameSound); // Odtw�rz d�wi�k
        Application.Quit();
    }

    // Uniwersalna metoda do odtwarzania d�wi�ku
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
