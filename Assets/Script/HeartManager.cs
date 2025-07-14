using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HeartManager : MonoBehaviour
{
    public static HeartManager Instance;

    [Header("Cấu hình máu")]
    [SerializeField] private int maxHearts = 3;

    private int currentHearts;

    [Header("Text hiển thị máu")]
    public TMP_Text heartText;

    void Awake()
    {
        // Đảm bảo singleton
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Luôn khởi tạo máu ngay tại Awake
        currentHearts = maxHearts;
    }

    void Start()
    {
        Debug.Log($"[HeartManager] maxHearts = {maxHearts}");
        currentHearts = maxHearts;
        UpdateUI();
    }


    public void LoseHeart(int amount = 1)
    {
        // Debug để tìm nơi gọi sai nếu có
        Debug.Log($"[HeartManager] Mất {amount} máu tại frame {Time.frameCount}");

        currentHearts -= amount;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);

        UpdateUI();

        if (currentHearts <= 0)
        {
            Debug.Log("[HeartManager] Hết máu! Chuyển sang scene EndMenu");
            SceneManager.LoadScene("EndMenu");
        }
    }

    void UpdateUI()
    {
        if (heartText != null)
        {
            heartText.text = currentHearts.ToString();
        }
        else
        {
            Debug.LogWarning("[HeartManager] Chưa gán TMP_Text!");
        }
    }

    public int GetCurrentHeart()
    {
        return currentHearts;
    }

    public void RestoreFull()
    {
        currentHearts = maxHearts;
        UpdateUI();
    }
}
