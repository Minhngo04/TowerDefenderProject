using UnityEngine;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    public GameObject upgradePanel;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI sellPriceText; // Text hiển thị giá bán
    
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
            // Đóng tất cả panel khác trước
            if (PanelManager.Instance != null)
            {
                PanelManager.Instance.CloseAllPanels();
            }
            
            // Kiểm tra xem panel đã mở chưa
            if (!upgradePanel.activeInHierarchy)
            {
                upgradePanel.SetActive(true);
                UpdateUpgradeUI();
                
                // Đăng ký với PanelManager
                if (PanelManager.Instance != null)
                {
                    PanelManager.Instance.RegisterPanel(upgradePanel);
                }
                
                Debug.Log($"📱 Upgrade panel opened for tower: {tower?.gameObject?.name ?? "Unknown"}");
            }
            else
            {
                Debug.Log($"📱 Upgrade panel already open for tower: {tower?.gameObject?.name ?? "Unknown"}");
                UpdateUpgradeUI(); // Cập nhật UI nếu cần
            }
        }
        else
        {
            Debug.LogWarning("⚠️ upgradePanel is null!");
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
            HideUpgradePanel(); // Đóng panel sau khi upgrade
        }
    }
    
    public void SellButtonClicked()
    {
        if (currentTower != null)
        {
            currentTower.SellTower();
            HideUpgradePanel(); // Đóng panel sau khi sell
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
            
        if (sellPriceText != null)
        {
            int sellPrice = currentTower.GetSellPrice();
            sellPriceText.text = $"Sell: {sellPrice}";
            Debug.Log($"📱 UI Update: Sell price text set to '{sellPriceText.text}'");
        }
        else
        {
            Debug.LogWarning("⚠️ sellPriceText is null! Please assign it in Inspector");
        }
    }
} 