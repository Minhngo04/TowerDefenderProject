using UnityEngine;
using UnityEngine.UI;

public class HammerHandler : MonoBehaviour
{
    public GameObject placeButtonPrefab; // Prefab của nút “Đặt turret”
    public Canvas canvas; // Canvas để hiển thị UI
    public Animator myAnimator; // Animator của camp hiện tại

    private GameObject activeButton;

    public void OnHammerClicked()
    {
        Debug.Log("Hammer clicked on: " + gameObject.name);

        // Nếu có button cũ thì xóa
        if (activeButton != null)
        {
            Destroy(activeButton);
        }

        // Tính vị trí button theo world-to-screen của Camp
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        activeButton = Instantiate(placeButtonPrefab, canvas.transform);
        activeButton.transform.position = screenPos + new Vector3(0, 50f, 0); // hơi lệch lên

        // Gán callback cho nút
        Button btn = activeButton.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            Debug.Log("Đặt turret tại: " + gameObject.name);
            if (myAnimator != null)
            {
                myAnimator.SetTrigger("Tower1");
            }
            else
            {
                Debug.LogWarning("Animator is null!");
            }

            // Xoá button sau khi bấm nếu muốn
            Destroy(activeButton);
        });
    }
}
