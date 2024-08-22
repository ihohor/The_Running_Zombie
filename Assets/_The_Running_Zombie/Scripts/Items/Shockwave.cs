using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] private float repelForce = 500f; // Si�a odpychania bomb

    // Funkcja wywo�ywana w momencie kolizji z bombami
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            Rigidbody2D bombRb = collision.GetComponent<Rigidbody2D>();

            if (bombRb != null)
            {
                // Oblicz kierunek odpychania (od �rodka fali uderzeniowej)
                Vector2 repelDirection = collision.transform.position - transform.position;
                repelDirection.Normalize();

                // Dodaj si�� odpychania
                bombRb.AddForce(repelDirection * repelForce);
            }
        }
    }
}
