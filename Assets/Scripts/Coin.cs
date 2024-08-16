using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Warto�� monety

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawd�, czy kolizja jest z obiektem maj�cym tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Dodaj monety do licznika
            CoinManager.Instance.AddCoin(coinValue);

            // Zniszcz monet�
            Destroy(gameObject);
        }
    }
}
