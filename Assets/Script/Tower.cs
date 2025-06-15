using UnityEngine;

public class CampClickLogger : MonoBehaviour
{
    // Hàm này sẽ được gọi khi đối tượng được click
    private void OnMouseDown()
    {
        Debug.Log("Click Unity Hub");
    }
}
