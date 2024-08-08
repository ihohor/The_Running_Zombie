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
        // Sprawdzanie, czy instancja ju¿ istnieje
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Zapewnia, ¿e obiekt nie zostanie zniszczony przy ³adowaniu nowych scen
        }
        else
        {
            Destroy(gameObject); // Usuwa nadmiarow¹ instancjê
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
        SceneManager.LoadScene("Level" + currentLevel + "Scene"); // £aduje scenê na podstawie numeru poziomu
    }

    public void NextLevel()
    {
        if (currentLevel < 10) // Za³ó¿my, ¿e masz 10 poziomów
        {
            currentLevel++;
            LoadLevel(currentLevel);
        }
        else
        {
            // Mo¿na dodaæ ekran zwyciêstwa lub inne akcje po ukoñczeniu ostatniego poziomu
            Debug.Log("Wszystkie poziomy ukoñczone!");
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
                    NextLevel(); // Zmieñ poziom po 2 sekundach trzymania dotyku
                    touchDuration = 0f; // Zresetuj czas dotyku
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchDuration = 0f; // Zresetuj czas dotyku, jeœli dotyk zosta³ przerwany
            }
        }
    }
}
