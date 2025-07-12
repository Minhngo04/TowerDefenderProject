using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public string name;
        public GameObject prefab;
        public int goldReward = 10;
        public float maxHealth = 100f;
        public float speed = 2f;
    }
    
    [Header("Enemy Types")]
    public EnemyType[] enemyTypes;
    
    /// <summary>
    /// Tạo enemy với thông tin tùy chỉnh
    /// </summary>
    public GameObject CreateEnemy(string enemyTypeName, Vector3 position)
    {
        EnemyType enemyType = GetEnemyTypeByName(enemyTypeName);
        if (enemyType == null)
        {
            Debug.LogWarning($"⚠️ Không tìm thấy enemy type: {enemyTypeName}");
            return null;
        }
        
        GameObject enemy = Instantiate(enemyType.prefab, position, Quaternion.identity);
        
        // Thêm EnemyData component nếu chưa có
        EnemyData enemyData = enemy.GetComponent<EnemyData>();
        if (enemyData == null)
        {
            enemyData = enemy.AddComponent<EnemyData>();
        }
        
        // Cập nhật thông tin enemy
        enemyData.enemyName = enemyType.name;
        enemyData.goldReward = enemyType.goldReward;
        enemyData.maxHealth = enemyType.maxHealth;
        enemyData.speed = enemyType.speed;
        
        // Cập nhật EnemyMovement nếu có
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.speed = enemyType.speed;
        }
        
        Debug.Log($"✅ Tạo enemy {enemyType.name} với {enemyType.goldReward} vàng reward");
        return enemy;
    }
    
    private EnemyType GetEnemyTypeByName(string name)
    {
        foreach (EnemyType type in enemyTypes)
        {
            if (type.name == name)
                return type;
        }
        return null;
    }
    
    /// <summary>
    /// Tạo enemy với thông tin tùy chỉnh trực tiếp
    /// </summary>
    public GameObject CreateCustomEnemy(GameObject prefab, Vector3 position, int goldReward, float maxHealth, float speed)
    {
        GameObject enemy = Instantiate(prefab, position, Quaternion.identity);
        
        // Thêm EnemyData component
        EnemyData enemyData = enemy.GetComponent<EnemyData>();
        if (enemyData == null)
        {
            enemyData = enemy.AddComponent<EnemyData>();
        }
        
        // Cập nhật thông tin
        enemyData.goldReward = goldReward;
        enemyData.maxHealth = maxHealth;
        enemyData.speed = speed;
        
        // Cập nhật EnemyMovement
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.speed = speed;
        }
        
        Debug.Log($"✅ Tạo custom enemy với {goldReward} vàng reward");
        return enemy;
    }
} 