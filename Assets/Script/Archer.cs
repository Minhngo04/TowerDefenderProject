using UnityEngine;

public class Archer : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    public void Attack()
    {
        animator.SetTrigger("DoAttack");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger hit: " + other.name); // thử log tất cả
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("🎯 Enemy detected → Archer attacks!");
            Attack();
        }
    }

}
