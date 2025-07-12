using UnityEngine;

[System.Serializable]
public class EnemyData : MonoBehaviour
{
    [Header("Enemy Information")]
    public string enemyName = "Enemy";
    public int goldReward = 10; // Số vàng nhận được khi enemy chết
    public float maxHealth = 100f;
    public float speed = 2f;
    
    [Header("Visual")]
    public Sprite enemySprite;
    public RuntimeAnimatorController animatorController;
    
    [Header("Audio")]
    public AudioClip deathSound;
    public AudioClip hitSound;
} 