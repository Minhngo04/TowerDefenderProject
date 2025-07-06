using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
        // Ẩn tất cả panel khi bắt đầu game
        HideAllPanelsOnStart();
    }
    
    void HideAllPanelsOnStart()
    {
        // Tìm và ẩn các panel cụ thể
        string[] panelNames = { "TowerOptionUI", "InfoPanel", "TowerPanel", "SelectionPanel" };
        
        foreach (string panelName in panelNames)
        {
            GameObject panel = GameObject.Find(panelName);
            if (panel != null)
            {
                panel.SetActive(false);
                Debug.Log($"Đã ẩn panel: {panelName}");
            }
        }
        
        // Ẩn tất cả panel con của Canvas
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                // Đây là Canvas con, ẩn nó
                canvas.gameObject.SetActive(false);
                Debug.Log($"Đã ẩn Canvas con: {canvas.name}");
            }
        }
        
        // Reset selectedCamp
        CampInfo.selectedCamp = null;
        
        // Sử dụng PanelManager nếu có
        if (PanelManager.Instance != null)
        {
            PanelManager.Instance.CloseAllPanels();
        }
    }
} 