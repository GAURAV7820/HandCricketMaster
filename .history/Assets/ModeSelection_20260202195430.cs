using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    public void SelectBattingOnly()
    {
        PlayerPrefs.SetInt("GameMode", 0); // 0 = Batting Only
        SceneManager.LoadScene("GameScene");
    }

    public void SelectFullMatch()
    {
        PlayerPrefs.SetInt("GameMode", 1); // 1 = Full Match
        SceneManager.LoadScene("GameScene");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}