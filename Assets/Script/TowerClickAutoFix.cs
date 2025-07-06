using UnityEngine;

public class TowerClickAutoFix : MonoBehaviour
{
    [Header("Auto Fix Settings")]
    public bool enableAutoFix = true;
    public float fixDelay = 0.1f; // Đợi 0.1 giây sau khi tower được tạo
    
    void Start()
    {
        if (enableAutoFix)
        {
            // Fix ngay lập tức
            Invoke("FixAllTowers", fixDelay);
        }
    }
    
    void FixAllTowers()
    {
        // Tìm tất cả tower trong scene
        Tower[] towers = FindObjectsOfType<Tower>();
        
        Debug.Log($"🔧 Auto-fixing {towers.Length} towers");
        
        foreach (Tower tower in towers)
        {
            FixTowerClickArea(tower.gameObject);
        }
    }
    
    void FixTowerClickArea(GameObject tower)
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
        
        // Tính toán kích thước vùng click mới (lớn gấp 2 lần sprite)
        Vector2 newClickSize = spriteSize * 2.0f;
        
        // Áp dụng kích thước mới
        boxCollider.size = newClickSize;
        boxCollider.offset = Vector2.zero;
        boxCollider.enabled = true;
        
        Debug.Log($"🎯 Auto-fixed {tower.name}: Click area = {newClickSize}");
    }
    
    // Phương thức để fix thủ công
    [ContextMenu("Fix All Towers Now")]
    public void FixAllTowersNow()
    {
        FixAllTowers();
    }
} 