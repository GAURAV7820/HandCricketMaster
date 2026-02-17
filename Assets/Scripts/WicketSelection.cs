using UnityEngine;
using UnityEngine.SceneManagement;

public class WicketSelection : MonoBehaviour
{
    public void SelectWickets(int wickets)
    {
        PlayerPrefs.SetInt("MaxWickets", wickets);
        SceneManager.LoadScene("GameScene");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("ModeSelection");
    }
}
