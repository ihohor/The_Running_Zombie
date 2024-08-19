using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndSceneScript : MonoBehaviour
{
    public GameObject bombPrefab;
    public Image flashImage;  // UI Image na ca�y ekran
    public Image zombieHead;  // Obraz g�owy zombie
    public Image greenSmear;  // Obraz zielonego �ladu po zombie

    private bool bombHitGround = false;

    void Start()
    {
        flashImage.gameObject.SetActive(false);
        zombieHead.gameObject.SetActive(false);
        greenSmear.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.collider.name); // Sprawdzamy, czy zachodzi kolizja

        if (collision.collider.CompareTag("Ground") && !bombHitGround)
        {
            Debug.Log("Bomb hit the ground!"); // Potwierdzamy, �e kolizja jest z ziemi�
            bombHitGround = true;

            // Wywo�aj efekt wybuchu
            StartCoroutine(TriggerExplosionEffects());
        }
    }

    IEnumerator TriggerExplosionEffects()
    {
        // W��cz efekt b�ysku
        flashImage.gameObject.SetActive(true);
        Color flashColor = flashImage.color;
        flashColor.a = 0;
        flashImage.color = flashColor;

        // Animacja b�ysku
        while (flashImage.color.a < 1)
        {
            flashColor.a += Time.deltaTime * 2; // Szybko�� b�ysku
            flashImage.color = flashColor;
            yield return null;
        }

        // Poczekaj 3 sekundy przed pojawieniem si� g�owy zombie
        yield return new WaitForSeconds(3f);

        // Wy�wietl g�ow� zombie
        zombieHead.gameObject.SetActive(true);

        // Ruch g�owy zombie w d�
        Vector3 startPosition = zombieHead.rectTransform.anchoredPosition;
        Vector3 endPosition = new Vector3(startPosition.x, -Screen.height / 4, startPosition.z);

        float elapsedTime = 0f;
        float moveDuration = 1f;

        while (elapsedTime < moveDuration)
        {
            zombieHead.rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        zombieHead.rectTransform.anchoredPosition = endPosition;

        // Poka� zielony �lad
        greenSmear.gameObject.SetActive(true);

        yield return new WaitForSeconds(10f);

        // Przejd� do menu g��wnego
        SceneManager.LoadScene("MainMenu");
    }
}
