using UnityEngine;

public class TowerClickHandler : MonoBehaviour
{
    [Header("Click Area Settings")]
    public float clickAreaMultiplier = 1.5f; // Nhân với kích thước sprite để tạo vùng click lớn hơn
    public bool useFixedClickArea = true; // Sử dụng vùng click cố định thay vì theo scale
    
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        // Lấy các component cần thiết
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (boxCollider == null)
        {
            Debug.LogError($"❌ BoxCollider2D not found on {gameObject.name}");
            return;
        }
        
        if (spriteRenderer == null)
        {
            Debug.LogError($"❌ SpriteRenderer not found on {gameObject.name}");
            return;
        }
        
        // Điều chỉnh vùng click
        AdjustClickArea();
    }
    
    void Awake()
    {
        // Khởi tạo sớm để đảm bảo vùng click hoạt động ngay lập tức
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
            
        if (boxCollider != null && spriteRenderer != null)
        {
            AdjustClickArea();
        }
    }
    
    void AdjustClickArea()
    {
        if (boxCollider == null || spriteRenderer == null) return;

        // Lấy kích thước thực tế của sprite (không tính transparent)
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector2 spriteCenter = spriteRenderer.sprite.bounds.center;

        // Tính lại vùng collider chỉ vừa với hình, không phóng to
        float customMultiplier = 1.0f; // Giữ đúng bằng hình, không nhân lên
        Vector2 fixedClickSize = spriteSize * customMultiplier;
        boxCollider.size = fixedClickSize;

        // Đặt offset collider đúng tâm hình
        boxCollider.offset = spriteCenter;

        // Đảm bảo collider được enable
        boxCollider.enabled = true;

        Debug.Log($"🎯 {gameObject.name}: Collider size set to {fixedClickSize}, offset {spriteCenter} (sprite: {spriteSize})");
    }
    
    // Phương thức để điều chỉnh lại vùng click khi cần thiết
    public void RefreshClickArea()
    {
        AdjustClickArea();
    }
    
    // Debug: Hiển thị vùng click trong Scene view
    void OnDrawGizmosSelected()
    {
        if (boxCollider != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 center = transform.position + (Vector3)boxCollider.offset;
            Vector3 size = new Vector3(boxCollider.size.x, boxCollider.size.y, 0.1f);
            Gizmos.DrawWireCube(center, size);
        }
    }
} 