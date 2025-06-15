using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed = 2f;

    private List<Vector3> fullPath = new();
    private HashSet<int> jumpPoints = new() { 2, 5 };

    private int currentPointIndex = 0;
    private Animator animator;
    private bool isJumping = false;
    private float jumpDuration = 0.5f;
    private float jumpTimer = 0f;

    public void SetPath(List<Vector3> path)
    {
        fullPath = path;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentPointIndex >= fullPath.Count)
        {
            animator?.SetBool("isJumping", false);
            Destroy(gameObject); // Hủy slime khi hoàn thành đường đi
            return;
        }

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

        Vector3 targetPos = fullPath[currentPointIndex];
        float distance = Vector3.Distance(transform.position, targetPos);
        Vector3 direction = (targetPos - transform.position).normalized;

        // Nếu đến điểm nhảy
        if (jumpPoints.Contains(currentPointIndex) && distance < 0.1f)
        {
            isJumping = true;
            animator?.SetBool("isJumping", true);
            return;
        }

        // Di chuyển đến điểm tiếp theo
        if (distance > 0.05f)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            currentPointIndex++;
        }

        // Lật hướng slime theo chiều di chuyển
        if (direction.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
