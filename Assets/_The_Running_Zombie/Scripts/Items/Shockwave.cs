using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] private float repelForce = 500f; // Si³a odpychania bomb

    // Funkcja wywo³ywana w momencie kolizji z bombami
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            Rigidbody2D bombRb = collision.GetComponent<Rigidbody2D>();

            if (bombRb != null)
            {
                // Oblicz kierunek odpychania (od œrodka fali uderzeniowej)
                Vector2 repelDirection = collision.transform.position - transform.position;
                repelDirection.Normalize();

                // Dodaj si³ê odpychania
                bombRb.AddForce(repelDirection * repelForce);
            }
        }
    }
}
