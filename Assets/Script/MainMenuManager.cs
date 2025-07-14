using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic;
    private AudioSource audioSource;
    public GameObject instructionPanel;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (menuMusic != null && menuMusic != null)
        {
            audioSource.clip = menuMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
    }

    public void OpenInstructionPanel()
    {
        if (instructionPanel != null)
            instructionPanel.SetActive(true);
    }
    public void setQuit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
