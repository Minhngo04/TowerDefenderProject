using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject towerOptionUI; // Gán trong Inspector
    public Vector2 uiOffset = new Vector2(0, 100f); // Đẩy UI lên một chút cho dễ thấy
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (towerOptionUI != null)
        {
            towerOptionUI.SetActive(false);
        }
        else
        {
            Debug.LogError("TowerOptionUI chưa được gán trong Inspector!");
        }
    }


    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("Click vào Camp: " + hit.collider.name);

                // Hiện UI
                if (towerOptionUI != null)
                {
                    towerOptionUI.SetActive(true);
                    Vector3 uiScreenPos = mainCamera.WorldToScreenPoint(transform.position);
                    towerOptionUI.transform.position = uiScreenPos + (Vector3)uiOffset;
                }
            }
            else
            {
                // Click ra ngoài thì ẩn UI
                if (towerOptionUI != null)
                {
                    towerOptionUI.SetActive(false);
                }
            }
        }
    }
}
