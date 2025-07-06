using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TowerClickSetup : MonoBehaviour
{
    [Header("Click Area Settings")]
    public float clickAreaMultiplier = 2.0f;
    public bool useFixedClickArea = true;
    public bool setupOnStart = true;
    
    void Start()
    {
        if (setupOnStart)
        {
            SetupAllTowers();
        }
    }
    
    [ContextMenu("Setup All Towers")]
    public void SetupAllTowers()
    {
        // Tìm tất cả tower trong scene
        Tower[] towers = FindObjectsOfType<Tower>();
        
        Debug.Log($"🔧 Setting up click areas for {towers.Length} towers");
        
        foreach (Tower tower in towers)
        {
            SetupTowerClickArea(tower.gameObject);
        }
    }
    
    public void SetupTowerClickArea(GameObject tower)
    {
        if (tower == null) return;
        
        // Lấy BoxCollider2D
        BoxCollider2D boxCollider = tower.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogWarning($"⚠️ No BoxCollider2D found on {tower.name}");
            return;
        }
        
        // Lấy SpriteRenderer
        SpriteRenderer spriteRenderer = tower.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogWarning($"⚠️ No SpriteRenderer or sprite found on {tower.name}");
            return;
        }
        
        // Lấy kích thước sprite
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        
        // Tính toán kích thước vùng click
        Vector2 clickSize;
        if (useFixedClickArea)
        {
            // Vùng click cố định, không phụ thuộc vào scale
            clickSize = spriteSize * clickAreaMultiplier;
        }
        else
        {
            // Vùng click theo scale hiện tại
            Vector2 currentScale = tower.transform.localScale;
            clickSize = spriteSize * currentScale * clickAreaMultiplier;
        }
        
        // Áp dụng kích thước mới
        boxCollider.size = clickSize;
        boxCollider.offset = Vector2.zero;
        boxCollider.enabled = true;
        
        Debug.Log($"🎯 {tower.name}: Click area set to {clickSize} (sprite: {spriteSize})");
    }
    
    // Phương thức để setup tower được chọn
    [ContextMenu("Setup Selected Tower")]
    public void SetupSelectedTower()
    {
        #if UNITY_EDITOR
        if (Selection.activeGameObject != null)
        {
            SetupTowerClickArea(Selection.activeGameObject);
        }
        else
        {
            Debug.LogWarning("Please select a tower first");
        }
        #else
        Debug.LogWarning("This method only works in Unity Editor");
        #endif
    }
} 