using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int coin = 70;
    public int maxCoin = 9999; // Giới hạn tối đa số xu
    public TextMeshProUGUI coinText; // Kéo thả Text UI vào đây trong Inspector

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        coin = Mathf.Clamp(coin, 0, maxCoin); // Giới hạn số xu từ 0 đến maxCoin
        UpdateCoinUI();
    }
    
    public bool CanAfford(int cost)
    {
        return coin >= cost;
    }
    
    public bool SpendCoin(int cost)
    {
        if (CanAfford(cost))
        {
            AddCoin(-cost);
            return true;
        }
        return false;
    }

    void UpdateCoinUI()
    {
        coinText.text = coin.ToString();
    }
} 