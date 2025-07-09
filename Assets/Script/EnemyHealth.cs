using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Image healthFill;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
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

        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoin(10); // Có thể cho từng enemy 1 số xu khác nhau sau này
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
