using UnityEngine;

public class CloseEntity : MonoBehaviour
{
    public GameObject entityToClose;

    public void Close()
    {
        if (entityToClose != null)
        {
            entityToClose.SetActive(false);
            
            // Hủy đăng ký panel với PanelManager
            if (PanelManager.Instance != null)
            {
                PanelManager.Instance.UnregisterPanel(entityToClose);
            }
        }
    }
}
