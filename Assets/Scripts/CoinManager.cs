using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public TextMeshProUGUI coinCounterTMP; // U¿ywamy TextMeshProUGUI zamiast Text
    private int coinCount = 0;

    private void Awake()
    {
        // Singleton pattern, ensures there's only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Preserve this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        coinCounterTMP.text = "Coins: " + coinCount;
    }
}
