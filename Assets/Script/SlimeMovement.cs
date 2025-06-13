using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed = 2f;

    private List<Vector3> path1 = new List<Vector3>();
    private List<Vector3> path2 = new List<Vector3>();
    private List<Vector3> fullPath = new List<Vector3>();

    private HashSet<int> jumpPoints = new HashSet<int> { 2, 5 }; // Các điểm slime sẽ nhảy

    private int currentPointIndex = 0;
    private Animator animator;
    private bool isJumping = false;
    private float jumpDuration = 0.5f;
    private float jumpTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Tạo đường đi
        path1.Add(new Vector3(-12, 1, 0));
        path1.Add(new Vector3(-6, 1, 0));
        path1.Add(new Vector3(-6, -3, 0));
        path1.Add(new Vector3(1, -3, 0));
        path1.Add(new Vector3(-1, -2, 0));
        path1.Add(new Vector3(11, -2, 0));

        path2.Add(new Vector3(-12, 1, 0));
        path2.Add(new Vector3(-6, 1, 0));
        path2.Add(new Vector3(-6, -3, 0));
        path2.Add(new Vector3(1, -3, 0));
        path2.Add(new Vector3(1, 2, 0));
        path2.Add(new Vector3(10, 2, 0));

        bool choosePath1 = Random.value > 0.5f;
        fullPath = choosePath1 ? path1 : path2;
    }

    void Update()
    {
        if (currentPointIndex >= fullPath.Count)
        {
            animator.SetBool("isJumping", false);
            return;
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpDuration)
            {
                isJumping = false;
                jumpTimer = 0f;
                animator.SetBool("isJumping", false);
                currentPointIndex++;
            }
            return;
        }

        Vector3 targetPos = fullPath[currentPointIndex];
        float distance = Vector3.Distance(transform.position, targetPos);
        Vector3 direction = (targetPos - transform.position).normalized;

        if (jumpPoints.Contains(currentPointIndex) && distance < 0.1f)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
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

        if (direction.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
