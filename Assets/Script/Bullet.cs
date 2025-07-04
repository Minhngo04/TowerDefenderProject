using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float Damage { get; set; }
    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
        Debug.Log($"[Bullet] Target set: {target}");
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Bay về phía target
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        // Nếu tới gần đủ, gây sát thương
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        Debug.Log("Bullet hit target!");

        // Tìm SlimeHealth ở target hoặc con của nó
        SlimeHealth enemyHealth = target.GetComponent<SlimeHealth>();
        if (enemyHealth == null)
        {
            enemyHealth = target.GetComponentInChildren<SlimeHealth>();
        }

        if (enemyHealth != null)
        {
            Debug.Log($"Enemy takes {Damage} damage");
            enemyHealth.TakeDamage(Damage);
        }
        else
        {
            Debug.LogWarning("Target does not have SlimeHealth component!");
        }

        Destroy(gameObject);
    }
}
