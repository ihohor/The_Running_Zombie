using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Wartoœæ monety

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SprawdŸ, czy kolizja jest z obiektem maj¹cym tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Dodaj monety do licznika
            CoinManager.Instance.AddCoin(coinValue);

            // Zniszcz monetê
            Destroy(gameObject);
        }
    }
}
