using UnityEngine;
using TMPro;
using System.Collections;

public class CoinNotification : MonoBehaviour
{
    public static CoinNotification Instance;
    public TextMeshProUGUI notificationText;
    public float displayTime = 2f;
    
    void Awake()
    {
        Instance = this;
    }
    
    public void ShowNotification(string message)
    {
        if (notificationText != null)
        {
            notificationText.text = message;
            StartCoroutine(HideNotificationAfterDelay());
        }
    }
    
    private IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        if (notificationText != null)
        {
            notificationText.text = "";
        }
    }
} 