using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    public void SelectBattingOnly()
    {
        PlayerPrefs.SetInt("GameMode", 0); // 0 = Batting Only
        SceneManager.LoadScene("WicketSelection");
    }

    public void SelectFullMatch()
    {
        PlayerPrefs.SetInt("GameMode", 1); // 1 = Full Match
        SceneManager.LoadScene("WicketSelection");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}