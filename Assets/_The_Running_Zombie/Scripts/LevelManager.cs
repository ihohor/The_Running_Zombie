using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private float timer = 0f;
    public float timeToSurvive = 300f;
    public TextMeshProUGUI timerText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subskrybuj zdarzenie ³adowania sceny
        UpdateTimerText();
    }

    void Update()
    {
        if (timerText != null)
        {
            timer += Time.deltaTime;

            if (timer >= timeToSurvive)
            {
                NextLevel();
            }

            UpdateTimerText();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Przypisz ponownie timerText po za³adowaniu nowej sceny
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            float timeRemaining = timeToSurvive - timer;
            int minutes = Mathf.FloorToInt(timeRemaining / 60F);
            int seconds = Mathf.FloorToInt(timeRemaining % 60F);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void NextLevel()
    {
        // Za³aduj kolejny poziom
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        timer = 0f; // Zresetuj timer
    }

    public void RestartLevel()
    {
        // Za³aduj aktualny poziom
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        timer = 0f; // Zresetuj timer
    }
}
