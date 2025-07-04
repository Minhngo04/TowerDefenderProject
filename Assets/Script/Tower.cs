using UnityEngine;

public class CampClickLogger : MonoBehaviour
{
    // Hàm này sẽ được gọi khi đối tượng được click
    private void OnMouseDown()
    {
        Debug.Log("Click Unity Hub");
    }
}

public class Tower : MonoBehaviour
{
    public float range = 5f;
    public float delayBtwAttacks = 2f;
    public float damage = 2f;
    public Transform projectileSpawnPosition;
    public GameObject bulletPrefab;

    private float _nextAttackTime;
    private GameObject _currentProjectileLoaded;

    void Update()
    {
        GameObject target = FindNearestEnemy();
        if (target != null)
        {
            // Xoay turret về phía enemy
            Vector3 enemyPos = target.transform.position - transform.position;
            float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
            transform.Rotate(0f, 0f, angle);

            // Bắn đạn nếu đã đến thời gian
            if (Time.time > _nextAttackTime)
            {
                Shoot(target);
                _nextAttackTime = Time.time + delayBtwAttacks;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= range)
            {
                shortestDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void Shoot(GameObject target)
    {
        if (bulletPrefab == null || projectileSpawnPosition == null) return;
        GameObject bullet = Instantiate(bulletPrefab, projectileSpawnPosition.position, transform.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null && target != null)
        {
            bulletScript.Seek(target.transform);
            bulletScript.Damage = damage;
        }
    }
}
