using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CampManager : MonoBehaviour
{
    public GameObject placeButtonPrefab; // Gán prefab PlaceButton ở đây
    public Canvas canvas; // Gán Canvas chính
    private Camera mainCamera;

    private List<GameObject> activeButtons = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            ShowButtonsOnCamps();
        }
    }

    void ShowButtonsOnCamps()
    {
        // Xoá nút cũ nếu có
        foreach (var btn in activeButtons)
        {
            Destroy(btn);
        }
        activeButtons.Clear();

        // Tìm tất cả Camp theo tag
        GameObject[] camps = GameObject.FindGameObjectsWithTag("Camp");

        foreach (GameObject camp in camps)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(camp.transform.position);

            GameObject btn = Instantiate(placeButtonPrefab, canvas.transform);
            btn.transform.position = screenPos + new Vector3(0, 50f, 0); // offset lên trên

            // Gán callback (nếu có script xử lý)
            btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                Debug.Log("Đặt turret tại: " + camp.name);
                // TODO: Gọi hàm đặt turret tại camp
            });

            activeButtons.Add(btn);
        }
    }
}
