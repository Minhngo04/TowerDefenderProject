using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic;
    private AudioSource audioSource;
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
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
