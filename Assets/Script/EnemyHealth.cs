using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Image healthFill;
    private Animator animator;
    private bool isDead = false;
    private EnemyData enemyData;

    void Start()
    {
        // Lấy EnemyData component
        enemyData = GetComponent<EnemyData>();
        
        // Nếu có EnemyData, sử dụng thông tin từ đó
        if (enemyData != null)
        {
            maxHealth = enemyData.maxHealth;
        }
        
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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
        isDead = true;
        gameObject.tag = "Dead"; // Đổi tag ngay khi chết
        // Giảm số enemy sống
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
            spawner.aliveEnemyCount--;

        // Thêm vàng dựa trên EnemyData
        int goldReward = 10; // Giá trị mặc định
        if (enemyData != null)
        {
            goldReward = enemyData.goldReward;
        }
        
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoin(goldReward);
            
            // Hiển thị reward text nếu có
            if (GoldRewardDisplay.Instance != null)
            {
                GoldRewardDisplay.Instance.ShowGoldReward(transform.position, goldReward);
            }
        }

        if (animator != null)
        {
            animator.SetBool("isDeath", true);
        }

        Destroy(gameObject, 2f);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
