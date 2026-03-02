using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Statistics:MonoBehaviour
{
    [Header("Texts")]
    public TMP_Text TotalMatchText;
    public TMP_Text WinsText;
    public TMP_Text LossesText;
    public TMP_Text HighestScoreText;
    public TMP_Text TotalRunsText;

    public GameObject mainMenuPanel;
    public GameObject statisticsPanel;

    void UpdateStats(){

        TotalMatchText.text="Total Matches: "+PlayerPrefs.GetInt("TotalMatches",0);
        WinsText.text="Total Wins: "+PlayerPrefs.GetInt("wins",0);
        LossesText.text="Total Losses: "+PlayerPrefs.GetInt("loss",0);
        HighestScoreText.text="Highest Score: "+PlayerPrefs.GetInt("highscore",0);
        TotalRunsText.text="Total Runs:"+PlayerPrefs.GetInt("totalruns",0);

    }

    public void OpenStats(){

         mainMenuPanel.SetActive(false);
        statisticsPanel.SetActive(true);
        UpdateStats();
    }

    public void CloseStats(){

        mainMenuPanel.SetActive(true);
        statisticsPanel.SetActive(false);
    }

}
