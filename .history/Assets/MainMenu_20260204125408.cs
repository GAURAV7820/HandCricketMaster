using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ModeSelection");
    }

    public void OpenCredits()
    {
        Debug.Log("Credits clicked");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited");
    }
}