using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TowerInitializer : MonoBehaviour
{
    [Header("Auto Setup Settings")]
    public bool autoSetupExistingTowers = true;
    public float clickAreaMultiplier = 2.0f;
    public bool useFixedClickArea = true;
    
    void Start()
    {
        if (autoSetupExistingTowers)
        {
            SetupExistingTowers();
        }
    }
    
    void SetupExistingTowers()
    {
        // Tìm tất cả tower có sẵn trong scene
        Tower[] existingTowers = FindObjectsOfType<Tower>();
        
        Debug.Log($"🔧 Found {existingTowers.Length} existing towers to setup");
        
        foreach (Tower tower in existingTowers)
        {
            SetupTowerClickHandler(tower.gameObject);
        }
    }
    
    public void SetupTowerClickHandler(GameObject tower)
    {
        if (tower == null) return;
        
        // Kiểm tra xem đã có TowerClickHandler chưa
        TowerClickHandler existingHandler = tower.GetComponent<TowerClickHandler>();
        if (existingHandler != null)
        {
            Debug.Log($"🎯 Tower {tower.name} already has TowerClickHandler");
            return;
        }
        
        // Thêm TowerClickHandler
        TowerClickHandler clickHandler = tower.AddComponent<TowerClickHandler>();
        clickHandler.clickAreaMultiplier = clickAreaMultiplier;
        clickHandler.useFixedClickArea = useFixedClickArea;
        
        Debug.Log($"🎯 Added TowerClickHandler to {tower.name}");
    }
    
    // Phương thức để setup thủ công từ Inspector
    [ContextMenu("Setup All Existing Towers")]
    public void SetupAllTowers()
    {
        SetupExistingTowers();
    }
    
    // Phương thức để setup một tower cụ thể
    [ContextMenu("Setup Selected Tower")]
    public void SetupSelectedTower()
    {
        #if UNITY_EDITOR
        if (Selection.activeGameObject != null)
        {
            SetupTowerClickHandler(Selection.activeGameObject);
        }
        #else
        Debug.LogWarning("This method only works in Unity Editor");
        #endif
    }
} 