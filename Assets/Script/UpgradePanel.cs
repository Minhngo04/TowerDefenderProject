using UnityEngine;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    public GameObject upgradePanel;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageText;
    
    private TowerUpgrade currentTower;
    
    void Start()
    {
        // Ẩn panel khi bắt đầu
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }
    
    public void ShowUpgradePanel(TowerUpgrade tower)
    {
        currentTower = tower;
        
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
            UpdateUpgradeUI();
            
            // Đăng ký với PanelManager
            if (PanelManager.Instance != null)
            {
                PanelManager.Instance.RegisterPanel(upgradePanel);
            }
        }
    }
    
    public void HideUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
            
            // Hủy đăng ký với PanelManager
            if (PanelManager.Instance != null)
            {
                PanelManager.Instance.UnregisterPanel(upgradePanel);
            }
        }
        
        currentTower = null;
    }
    
    public void UpgradeButtonClicked()
    {
        if (currentTower != null)
        {
            currentTower.UpgradeTower();
            UpdateUpgradeUI();
        }
    }
    
    void UpdateUpgradeUI()
    {
        if (currentTower == null) return;
        
        Tower tower = currentTower.GetComponent<Tower>();
        if (tower == null) return;
        
        if (costText != null)
            costText.text = "Cost: " + currentTower.GetUpgradeCost();
            
        if (levelText != null)
            levelText.text = "Level: " + currentTower.GetCurrentLevel();
            
        if (damageText != null)
            damageText.text = "Damage: " + tower.damage.ToString("F1");
    }
} 