using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Dynamic;

public class CampManager : MonoBehaviour
{
    public GameObject hammerIconPrefab;
    public Canvas canvas;

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
            ShowHammerIconsOnCamps();
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

                if (campAnimator != null)
                {
                    campAnimator.SetTrigger(triggerName);
                }
                else
                {
                    Debug.LogWarning("Animator not found on " + camp.name);
                }

                Destroy(icon);
            });

            activeIcons.Add(icon);
        }
    }
}