using UnityEngine;

public class CloseEntity : MonoBehaviour
{
    public GameObject entityToClose;

    public void Close()
    {
        if (entityToClose != null)
        {
            entityToClose.SetActive(false);
        }
    }
}
