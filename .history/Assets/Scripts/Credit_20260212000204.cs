using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
        public AudioSource audioSource;
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}