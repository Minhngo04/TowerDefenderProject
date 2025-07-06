using UnityEngine;
using UnityEngine.UI;

public class SlimeHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Image healthFill; // Kéo vào từ Inspector

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"[SlimeHealth] Took {amount} damage. Current health: {currentHealth}");

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("[SlimeHealth] Enemy died.");
        
        // Thêm đồng xu khi tiêu diệt kẻ địch
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoin(10); // Thưởng 10 xu cho mỗi slime
        }
        
        Destroy(gameObject); // hoặc phát hiệu ứng chết
    }
}
