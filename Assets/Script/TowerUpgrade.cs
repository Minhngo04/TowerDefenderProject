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
    
    void Start()
    {
        tower = GetComponent<Tower>();
        UpdateUI();
    }
    
    public void UpgradeTower()
    {
        if (CoinManager.Instance != null && CoinManager.Instance.SpendCoin(upgradeCost))
        {
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
} 