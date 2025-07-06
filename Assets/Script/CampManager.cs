using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;

public class CampManager : MonoBehaviour
{
    public GameObject hammerIconPrefab;
    public Canvas canvas;
    public int towerCost = 30; // Chi phí mua tháp
    public TextMeshProUGUI costText; // Hiển thị chi phí

    private Camera mainCamera;
    private List<GameObject> activeIcons = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            // Đóng tất cả panel đang mở
            CloseAllPanels();
        }
    }
    
    void CloseAllPanels()
    {
        // Đóng tất cả panel thông qua PanelManager
        if (PanelManager.Instance != null)
        {
            PanelManager.Instance.CloseAllPanels();
        }
        
        // Đóng panel có sẵn nếu đang mở
        if (CampInfo.selectedCamp != null && CampInfo.selectedCamp.infoPanel != null)
        {
            CampInfo.selectedCamp.infoPanel.SetActive(false);
            CampInfo.selectedCamp = null;
        }
        
        // Xóa tất cả icon búa nếu có
        ClearAllIcons();
    }
    
    // Phương thức để hiển thị panel cho camp cụ thể (có thể gọi từ nơi khác)
    public void ShowPanelForCamp(CampInfo camp)
    {
        if (camp != null && camp.infoPanel != null)
        {
            CloseAllPanels(); // Đóng panel khác trước
            CampInfo.selectedCamp = camp;
            camp.infoPanel.SetActive(true);
        }
    }

    void ShowHammerIconsOnCamps()
    {
        foreach (var icon in activeIcons)
        {
            Destroy(icon);
        }
        activeIcons.Clear();

        GameObject[] camps = GameObject.FindGameObjectsWithTag("Camp");

        foreach (GameObject camp in camps)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(camp.transform.position);

            GameObject icon = Instantiate(hammerIconPrefab, canvas.transform);
            icon.transform.position = screenPos + new Vector3(0, 50f, 0);

            Animator campAnimator = camp.GetComponent<Animator>();
            CampInfo campInfo = camp.GetComponent<CampInfo>();

            string triggerName = (campInfo != null) ? campInfo.triggerName : "Tower1";

            icon.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Click hammer on camp: " + camp.name + " → Trigger: " + triggerName);

                // Kiểm tra đủ tiền để mua tháp
                if (CoinManager.Instance != null && CoinManager.Instance.SpendCoin(towerCost))
                {
                    
                    // Xây tháp
                    if (campAnimator != null)
                    {
                        campAnimator.SetTrigger(triggerName);
                    }
                    else
                    {
                        Debug.LogWarning("Animator not found on " + camp.name);
                    }
                    
                    Debug.Log($"Đã mua tháp với giá {towerCost}. Số xu còn lại: {CoinManager.Instance.coin}");
                }
                else
                {
                    string message = "Không đủ tiền! Cần " + towerCost;
                    Debug.Log(message);
                    if (CoinNotification.Instance != null)
                    {
                        CoinNotification.Instance.ShowNotification(message);
                    }
                }

                // Xóa tất cả các icon sau khi click
                ClearAllIcons();
            });

            activeIcons.Add(icon);
        }
    }
    
    void ClearAllIcons()
    {
        foreach (var icon in activeIcons)
        {
            if (icon != null)
            {
                Destroy(icon);
            }
        }
        activeIcons.Clear();
    }
}