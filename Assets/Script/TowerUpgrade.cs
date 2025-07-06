using UnityEngine;
using TMPro;

public class TowerUpgrade : MonoBehaviour
{
    public int upgradeCost = 50;
    public float damageIncrease = 10f;
    public GameObject upgradePanel;
    
    public TextMeshProUGUI costText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageText;
    
    private Tower tower;
    private int currentLevel = 1;
    private int originalCost = 0; // Giá mua ban đầu
    private int totalUpgradeCost = 0; // Tổng chi phí đã bỏ ra để upgrade
    
    void Start()
    {
        tower = GetComponent<Tower>();
        
        // Lấy giá mua ban đầu từ TowerData
        TowerData towerData = GetComponent<TowerData>();
        if (towerData != null)
        {
            originalCost = towerData.cost;
            Debug.Log($"💰 Tower {gameObject.name}: Original cost = {originalCost}");
        }
        else
        {
            Debug.LogWarning($"⚠️ TowerData not found on {gameObject.name}");
        }
        
        UpdateUI();
    }
    
    public void UpgradeTower()
    {
        if (CoinManager.Instance != null && CoinManager.Instance.SpendCoin(upgradeCost))
        {
            // Cộng dồn chi phí upgrade
            totalUpgradeCost += upgradeCost;
            
            // Tăng level
            currentLevel++;
            
            // Tăng damage
            if (tower != null)
            {
                tower.damage += damageIncrease;
            }
            
            // Tăng chi phí upgrade tiếp theo
            upgradeCost = (int)(upgradeCost * 1.5f);
            
            UpdateUI();
            
            Debug.Log($"✅ Tower upgraded to level {currentLevel}! New damage: {tower.damage}");
        }
        else
        {
            string message = "Không đủ tiền! Cần " + upgradeCost;
            Debug.Log(message);
            if (CoinNotification.Instance != null)
            {
                CoinNotification.Instance.ShowNotification(message);
            }
        }
    }
    
    public void SellTower()
    {
        // Tính giá bán theo level
        int sellPrice = GetSellPrice();
        
        // Thêm tiền vào CoinManager
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoin(sellPrice);
            
            // Tìm CampInfo để reset trạng thái
            CampInfo campInfo = GetComponentInParent<CampInfo>();
            if (campInfo != null)
            {
                campInfo.ResetCamp();
            }
            
            // Xóa tower
            Destroy(gameObject);
            
            Debug.Log($"💰 Tower sold for {sellPrice} coins! (Level {currentLevel}, Original: {originalCost})");
        }
    }
    
    void UpdateUI()
    {
        if (costText != null)
            costText.text = upgradeCost.ToString();
            
        if (levelText != null)
            levelText.text = "Level " + currentLevel;
            
        if (damageText != null && tower != null)
            damageText.text = "Damage: " + tower.damage.ToString("F1");
    }
    
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    
    public int GetUpgradeCost()
    {
        return upgradeCost;
    }
    
    public int GetSellPrice()
    {
        // Giá bán theo level: Level 1 = 50%, Level 2 = 75%, Level 3 = 100%, Level 4+ = 125%
        float sellPercentage = 0.5f; // 50% cho level 1
        
        if (currentLevel >= 2) sellPercentage = 0.75f;      // 75% cho level 2
        if (currentLevel >= 3) sellPercentage = 1.0f;       // 100% cho level 3  
        if (currentLevel >= 4) sellPercentage = 1.25f;      // 125% cho level 4+
        
        int sellPrice = Mathf.RoundToInt(originalCost * sellPercentage);
        
        Debug.Log($"💰 GetSellPrice: Level={currentLevel}, Original={originalCost}, Percentage={sellPercentage*100}%, Sell={sellPrice}");
        return sellPrice;
    }
    
    public int GetTotalInvestment()
    {
        return originalCost + totalUpgradeCost;
    }
} 