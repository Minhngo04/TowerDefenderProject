using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 5f;
    public float delayBtwAttacks = 2f;
    public float damage = 2f;
    public Transform projectileSpawnPosition;
    public GameObject bulletPrefab;

    private float _nextAttackTime;
    private GameObject _currentProjectileLoaded;
    private TowerUpgrade towerUpgrade;
    
    void Start()
    {
        towerUpgrade = GetComponent<TowerUpgrade>();
    }

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
    
    private void OnMouseDown()
    {
        Debug.Log($"🖱️ Tower {gameObject.name} clicked");
        
        if (towerUpgrade != null && towerUpgrade.upgradePanel != null)
        {
            Debug.Log($"✅ Opening upgrade panel for {gameObject.name}");
            towerUpgrade.upgradePanel.SetActive(true);
            UpgradePanel panelScript = towerUpgrade.upgradePanel.GetComponent<UpgradePanel>();
            if (panelScript != null)
            {
                panelScript.ShowUpgradePanel(towerUpgrade);
            }
            else
            {
                Debug.LogWarning($"⚠️ UpgradePanel script not found on {towerUpgrade.upgradePanel.name}");
            }
        }
        else
        {
            Debug.LogWarning($"⚠️ Cannot open upgrade panel: towerUpgrade={towerUpgrade}, upgradePanel={towerUpgrade?.upgradePanel}");
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