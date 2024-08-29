using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    public GameObject pauseButton;

    // Dodaj referencjê do pliku muzycznego, który ma graæ podczas pauzy
    public AudioClip pauseMusicClip; // Plik muzyczny na pauzê

    private AudioSource musicSource;  // Automatycznie dodamy AudioSource
    private AudioSource[] allAudioSources;

    void Start()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);

        // Pobierz wszystkie Ÿród³a dŸwiêku w scenie
        allAudioSources = FindObjectsOfType<AudioSource>();

        // Dodajemy AudioSource dynamicznie, aby odtwarzaæ muzykê pauzy
        if (pauseMusicClip != null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = pauseMusicClip;
            musicSource.loop = true;  // Ustaw muzykê na pêtlê, jeœli chcesz, ¿eby gra³a w kó³ko
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;

        // Wycisz wszystkie dŸwiêki, oprócz muzyki pauzy
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != musicSource)
            {
                audioSource.Pause();
            }
        }

        // Odtwórz muzykê pauzy
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

        // Wznów wszystkie dŸwiêki
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != musicSource)
            {
                audioSource.UnPause();
            }
        }

        // Zatrzymaj muzykê pauzy
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
