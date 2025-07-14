using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;

    private List<Vector3> fullPath = new();
    private HashSet<int> jumpPoints = new() { 2, 5 };

    private int currentPointIndex = 0;
    private Animator animator;
    private bool isJumping = false;
    private float jumpDuration = 0.5f;
    private float jumpTimer = 0f;

    private EnemyHealth enemyHealth;
    private float originalXScale;

    private bool pathSet = false;

    public void SetPath(List<Vector3> path)
    {
        fullPath = path;
        currentPointIndex = 0;
        pathSet = true;

        Debug.Log($"[EnemyMovement] {gameObject.name} được gán path có {path.Count} điểm.");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        originalXScale = Mathf.Abs(transform.localScale.x);

        Debug.Log($"[EnemyMovement] {gameObject.name} Start() tại frame {Time.frameCount}");
    }

    void Update()
    {
        // Nếu chưa có path → không làm gì cả
        if (!pathSet || fullPath.Count == 0)
        {
            return;
        }

        // Nếu enemy chết → không di chuyển
        if (enemyHealth != null && enemyHealth.IsDead())
        {
            animator?.SetBool("isJumping", false);
            return;
        }

        // Nếu đi hết path
        if (currentPointIndex >= fullPath.Count)
        {
            Debug.Log($"[EnemyMovement] {gameObject.name} đến hết path tại frame {Time.frameCount}");

            if (enemyHealth != null && !enemyHealth.IsDead())
            {
                Debug.Log($"[EnemyMovement] {gameObject.name} gây mất máu");
                HeartManager.Instance?.LoseHeart(1);
            }

            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null)
            {
                spawner.NotifyEnemyDeath();
            }

            Destroy(gameObject);
            return;
        }

        // Xử lý nhảy
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpDuration)
            {
                isJumping = false;
                jumpTimer = 0f;
                animator?.SetBool("isJumping", false);
                currentPointIndex++;
            }
            return;
        }

        // Di chuyển đến điểm
        Vector3 targetPos = fullPath[currentPointIndex];
        float distance = Vector3.Distance(transform.position, targetPos);
        Vector3 direction = (targetPos - transform.position).normalized;

        if (jumpPoints.Contains(currentPointIndex) && distance < 0.1f)
        {
            isJumping = true;
            animator?.SetBool("isJumping", true);
            return;
        }

        if (distance > 0.05f)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            currentPointIndex++;
        }

        // Lật hướng
        if (direction.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Sign(direction.x) * originalXScale;
            transform.localScale = scale;
        }
    }
}
