using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private int currentLevel = 1;
    private float touchDuration = 0f;
    private float requiredTouchTime = 2f; // Czas wymagany do zmiany poziomu (2 sekundy)

    void Awake()
    {
        // Sprawdzanie, czy instancja ju� istnieje
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Zapewnia, �e obiekt nie zostanie zniszczony przy �adowaniu nowych scen
        }
        else
        {
            Destroy(gameObject); // Usuwa nadmiarow� instancj�
        }
    }

    void Start()
    {
        LoadLevel(currentLevel);
    }

    void Update()
    {
        HandleTouchInput();
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene("Level" + currentLevel + "Scene"); // �aduje scen� na podstawie numeru poziomu
    }

    public void NextLevel()
    {
        if (currentLevel < 10) // Za��my, �e masz 10 poziom�w
        {
            currentLevel++;
            LoadLevel(currentLevel);
        }
        else
        {
            // Mo�na doda� ekran zwyci�stwa lub inne akcje po uko�czeniu ostatniego poziomu
            Debug.Log("Wszystkie poziomy uko�czone!");
        }
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary)
            {
                touchDuration += Time.deltaTime;

                if (touchDuration >= requiredTouchTime)
                {
                    NextLevel(); // Zmie� poziom po 2 sekundach trzymania dotyku
                    touchDuration = 0f; // Zresetuj czas dotyku
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchDuration = 0f; // Zresetuj czas dotyku, je�li dotyk zosta� przerwany
            }
        }
    }
}
