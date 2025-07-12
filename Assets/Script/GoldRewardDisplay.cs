using UnityEngine;
using TMPro;
using System.Collections;

public class GoldRewardDisplay : MonoBehaviour
{
    public static GoldRewardDisplay Instance;
    
    [Header("UI Components")]
    public GameObject rewardTextPrefab;
    public Canvas canvas;
    
    [Header("Settings")]
    public float displayTime = 2f;
    public float moveSpeed = 50f;
    public Color textColor = Color.yellow;
    
    void Awake()
    {
        Instance = this;
    }
    
    /// <summary>
    /// Hiển thị số vàng nhận được tại vị trí enemy
    /// </summary>
    public void ShowGoldReward(Vector3 worldPosition, int goldAmount)
    {
        if (rewardTextPrefab == null || canvas == null)
        {
            Debug.LogWarning("⚠️ rewardTextPrefab hoặc canvas chưa được gán!");
            return;
        }
        
        // Chuyển đổi vị trí world sang screen
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        
        // Tạo text UI
        GameObject rewardText = Instantiate(rewardTextPrefab, canvas.transform);
        rewardText.transform.position = screenPos;
        
        // Cập nhật text
        TextMeshProUGUI textComponent = rewardText.GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = $"+{goldAmount}";
            textComponent.color = textColor;
        }
        
        // Chạy animation
        StartCoroutine(AnimateRewardText(rewardText));
    }
    
    private IEnumerator AnimateRewardText(GameObject rewardText)
    {
        Vector3 startPos = rewardText.transform.position;
        Vector3 endPos = startPos + Vector3.up * moveSpeed;
        
        float elapsed = 0f;
        
        while (elapsed < displayTime)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / displayTime;
            
            // Di chuyển lên trên
            rewardText.transform.position = Vector3.Lerp(startPos, endPos, progress);
            
            // Fade out
            TextMeshProUGUI textComponent = rewardText.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                Color color = textComponent.color;
                color.a = 1f - progress;
                textComponent.color = color;
            }
            
            yield return null;
        }
        
        Destroy(rewardText);
    }
} 