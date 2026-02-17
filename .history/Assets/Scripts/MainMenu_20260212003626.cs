using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayGame()
    {
        SceneManager.LoadScene("ModeSelection");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited");
    }
}