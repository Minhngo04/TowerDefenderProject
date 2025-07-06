using UnityEngine;

public class TowerClickFixer : MonoBehaviour
{
    [Header("Auto Fix Settings")]
    public bool autoFixOnStart = true;
    public float checkInterval = 0.1f; // Kiểm tra mỗi 0.1 giây
    public float maxCheckTime = 2.0f; // Tối đa kiểm tra trong 2 giây
    
    private float checkTimer = 0f;
    private float totalCheckTime = 0f;
    
    void Start()
    {
        if (autoFixOnStart)
        {
            // Fix ngay lập tức
            FixTowerClickArea();
        }
    }
    
    void Update()
    {
        if (autoFixOnStart && totalCheckTime < maxCheckTime)
        {
            checkTimer += Time.deltaTime;
            totalCheckTime += Time.deltaTime;
            
            if (checkTimer >= checkInterval)
            {
                checkTimer = 0f;
                FixTowerClickArea();
            }
        }
    }
    
    void FixTowerClickArea()
    {
        // Tìm tất cả tower trong scene
        Tower[] towers = FindObjectsOfType<Tower>();
        
        foreach (Tower tower in towers)
        {
            FixSingleTower(tower.gameObject);
        }
    }
    
    void FixSingleTower(GameObject tower)
    {
        if (tower == null) return;
        
        // Kiểm tra BoxCollider2D
        BoxCollider2D boxCollider = tower.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogWarning($"⚠️ No BoxCollider2D found on {tower.name}");
            return;
        }
        
        // Kiểm tra SpriteRenderer
        SpriteRenderer spriteRenderer = tower.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogWarning($"⚠️ No SpriteRenderer or sprite found on {tower.name}");
            return;
        }
        
        // Kiểm tra xem vùng click có quá nhỏ không
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector2 currentColliderSize = boxCollider.size;
        
        // Nếu collider quá nhỏ (nhỏ hơn 1.5 lần sprite size), sửa lại
        if (currentColliderSize.x < spriteSize.x * 1.5f || currentColliderSize.y < spriteSize.y * 1.5f)
        {
            // Tính toán kích thước mới
            Vector2 newSize = spriteSize * 2.0f; // Vùng click lớn gấp 2 lần sprite
            
            // Áp dụng kích thước mới
            boxCollider.size = newSize;
            boxCollider.offset = Vector2.zero;
            boxCollider.enabled = true;
            
            Debug.Log($"🔧 Fixed click area for {tower.name}: {currentColliderSize} -> {newSize}");
        }
        
        // Đảm bảo collider được enable
        if (!boxCollider.enabled)
        {
            boxCollider.enabled = true;
            Debug.Log($"🔧 Enabled BoxCollider2D for {tower.name}");
        }
    }
    
    [ContextMenu("Fix All Towers Now")]
    public void FixAllTowersNow()
    {
        FixTowerClickArea();
    }
    
    [ContextMenu("Fix Selected Tower")]
    public void FixSelectedTower()
    {
        #if UNITY_EDITOR
        if (UnityEditor.Selection.activeGameObject != null)
        {
            FixSingleTower(UnityEditor.Selection.activeGameObject);
        }
        #else
        Debug.LogWarning("This method only works in Unity Editor");
        #endif
    }
} 