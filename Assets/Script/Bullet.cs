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

        // Đạn luôn bay về phía enemy (homing)
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        // TODO: Gây sát thương cho enemy ở đây nếu muốn
        Destroy(gameObject);
    }
} 