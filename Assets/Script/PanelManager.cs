using UnityEngine;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    
    private List<GameObject> activePanels = new List<GameObject>();
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        // Đảm bảo tất cả panel được ẩn khi bắt đầu game
        CloseAllPanels();
    }
    
    public void RegisterPanel(GameObject panel)
    {
        if (panel != null && !activePanels.Contains(panel))
        {
            activePanels.Add(panel);
        }
    }
    
    public void UnregisterPanel(GameObject panel)
    {
        if (panel != null && activePanels.Contains(panel))
        {
            activePanels.Remove(panel);
        }
    }
    
    public void CloseAllPanels()
    {
        foreach (GameObject panel in activePanels.ToArray())
        {
            if (panel != null && panel.activeInHierarchy)
            {
                panel.SetActive(false);
            }
        }
        activePanels.Clear();
    }
    
    public bool HasActivePanels()
    {
        return activePanels.Count > 0;
    }
} 