using UnityEngine;

public class CampInfo : MonoBehaviour
{
    public string triggerName = "Tower1";           // Tên trigger (tùy chọn)
    public GameObject infoPanel;                    // Panel chọn loại pháo

    [HideInInspector] public static CampInfo selectedCamp; // Camp đang được chọn
    private bool isBuilt = false;                   // Kiểm tra đã xây chưa

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

        // Tạo pháo tại vị trí CampBase
        GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity, transform);

        // ✅ Thu nhỏ pháo để phù hợp (chỉnh scale tùy ý bạn)
        tower.transform.localScale = new Vector3(0.2082919f, 0.2293043f, 1f);

        isBuilt = true;

        Debug.Log($"✅ Tower built on {gameObject.name}");

        // Ẩn panel sau khi chọn
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }
}
