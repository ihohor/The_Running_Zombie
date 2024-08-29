using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    public GameObject pauseButton;

    // Dodaj referencj� do pliku muzycznego, kt�ry ma gra� podczas pauzy
    public AudioClip pauseMusicClip; // Plik muzyczny na pauz�

    private AudioSource musicSource;  // Automatycznie dodamy AudioSource
    private AudioSource[] allAudioSources;

    void Start()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);

        // Pobierz wszystkie �r�d�a d�wi�ku w scenie
        allAudioSources = FindObjectsOfType<AudioSource>();

        // Dodajemy AudioSource dynamicznie, aby odtwarza� muzyk� pauzy
        if (pauseMusicClip != null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = pauseMusicClip;
            musicSource.loop = true;  // Ustaw muzyk� na p�tl�, je�li chcesz, �eby gra�a w k�ko
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;

        // Wycisz wszystkie d�wi�ki, opr�cz muzyki pauzy
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != musicSource)
            {
                audioSource.Pause();
            }
        }

        // Odtw�rz muzyk� pauzy
        if (musicSource != null)
        {
            musicSource.Play();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;

        // Wzn�w wszystkie d�wi�ki
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != musicSource)
            {
                audioSource.UnPause();
            }
        }

        // Zatrzymaj muzyk� pauzy
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
}
