using UnityEngine;

public class CampInfo : MonoBehaviour
{
    public string triggerName = "Tower1";           // Tên trigger (tùy chọn)
    public GameObject infoPanel;                    // Panel chọn loại pháo

    [HideInInspector] public static CampInfo selectedCamp; // Camp đang được chọn
    private bool isBuilt = false;                   // Kiểm tra đã xây chưa

    void Start()
    {
        // Đảm bảo panel được ẩn khi bắt đầu game
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        // Không cho chọn lại nếu đã xây
        if (isBuilt) return;

        // Đánh dấu camp đang được chọn
        selectedCamp = this;

        Debug.Log($"🟨 CampBase clicked: {gameObject.name}");

        // Hiện panel chọn pháo nếu có
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
            
            // Đăng ký panel với PanelManager
            if (PanelManager.Instance != null)
            {
                PanelManager.Instance.RegisterPanel(infoPanel);
            }
        }
        else
        {
            Debug.LogWarning("⚠️ InfoPanel chưa được gán trong Inspector!");
        }
    }

    /// <summary>
    /// Hàm gọi từ panel để xây pháo tại camp này
    /// </summary>
    /// <param name="towerPrefab">Prefab của pháo</param>
    public void BuildTower(GameObject towerPrefab)
    {
        if (isBuilt || towerPrefab == null) return;

        // Lấy giá từ TowerData trên prefab (nếu có)
        int towerCost = 30; // Giá mặc định
        TowerData data = towerPrefab.GetComponent<TowerData>();
        if (data != null)
            towerCost = data.cost;

        // Kiểm tra đủ tiền để mua tháp
        if (CoinManager.Instance != null && CoinManager.Instance.SpendCoin(towerCost))
        {
            // Tạo pháo tại vị trí CampBase
            GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity, transform);

            // ✅ Thu nhỏ pháo để phù hợp (chỉnh scale tùy ý bạn)
            tower.transform.localScale = new Vector3(0.2082919f, 0.2293043f, 1f);

            isBuilt = true;

            Debug.Log($"✅ Tower built on {gameObject.name} with cost {towerCost}");
        }
        else
        {
            string message = "Không đủ tiền! Cần " + towerCost;
            Debug.Log(message);
            if (CoinNotification.Instance != null)
            {
                CoinNotification.Instance.ShowNotification(message);
            }
            return; // Không xây tháp nếu không đủ tiền
        }

        // Ẩn panel sau khi chọn
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
            
            // Hủy đăng ký panel với PanelManager
            if (PanelManager.Instance != null)
            {
                PanelManager.Instance.UnregisterPanel(infoPanel);
            }
        }
    }
}
