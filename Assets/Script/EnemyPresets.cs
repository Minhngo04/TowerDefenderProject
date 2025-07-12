using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPreset", menuName = "Tower Defense/Enemy Preset")]
public class EnemyPreset : ScriptableObject
{
    [System.Serializable]
    public class EnemyPresetData
    {
        [Header("Basic Info")]
        public string enemyName;
        public GameObject prefab;
        
        [Header("Stats")]
        public int goldReward = 10;
        public float maxHealth = 100f;
        public float speed = 2f;
        
        [Header("Visual")]
        public Sprite sprite;
        public RuntimeAnimatorController animatorController;
        
        [Header("Audio")]
        public AudioClip deathSound;
        public AudioClip hitSound;
    }
    
    [Header("Enemy Presets")]
    public EnemyPresetData[] presets;
    
    /// <summary>
    /// Lấy preset theo tên
    /// </summary>
    public EnemyPresetData GetPresetByName(string name)
    {
        foreach (EnemyPresetData preset in presets)
        {
            if (preset.enemyName == name)
                return preset;
        }
        return null;
    }
    
    /// <summary>
    /// Tạo enemy từ preset
    /// </summary>
    public GameObject CreateEnemyFromPreset(string presetName, Vector3 position)
    {
        EnemyPresetData preset = GetPresetByName(presetName);
        if (preset == null)
        {
            Debug.LogWarning($"⚠️ Không tìm thấy preset: {presetName}");
            return null;
        }
        
        GameObject enemy = Instantiate(preset.prefab, position, Quaternion.identity);
        
        // Thêm EnemyData
        EnemyData enemyData = enemy.GetComponent<EnemyData>();
        if (enemyData == null)
        {
            enemyData = enemy.AddComponent<EnemyData>();
        }
        
        // Cập nhật thông tin từ preset
        enemyData.enemyName = preset.enemyName;
        enemyData.goldReward = preset.goldReward;
        enemyData.maxHealth = preset.maxHealth;
        enemyData.speed = preset.speed;
        enemyData.enemySprite = preset.sprite;
        enemyData.animatorController = preset.animatorController;
        enemyData.deathSound = preset.deathSound;
        enemyData.hitSound = preset.hitSound;
        
        // Cập nhật EnemyMovement
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.speed = preset.speed;
        }
        
        Debug.Log($"✅ Tạo enemy {preset.enemyName} từ preset với {preset.goldReward} vàng reward");
        return enemy;
    }
} 