using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Text")]
    public TMP_Text turnText;
    public TMP_Text scoreText;
    public TMP_Text computerText;

    [Header("UI Objects")]
    public GameObject restartButton;
    public GameObject NotOut_Image;
    public GameObject Wicket_Image;

    [Header("Game Settings")]
    public int maxWickets = 5;   

    int playerScore = 0;
    int wickets = 0;

    void Start()
    {
        maxWickets = PlayerPrefs.GetInt("MaxWickets", 5);
        ResetGame();
    }
    public void OnNumberPressed(int playerNumber)
    {
        if (wickets >= maxWickets)
            return;
        int computerNumber = Random.Range(0, 7);
        computerText.text = "Computer chose: " + computerNumber;
        if (playerNumber == computerNumber)
        {
            wickets++;
            NotOut_Image.SetActive(false);
            Wicket_Image.SetActive(true);
            if (wickets >= maxWickets)
            {
                turnText.text = "ALL OUT!\nFinal Score: " + playerScore;
                restartButton.SetActive(true);
            }
            else
            {
                turnText.text = "OUT!\nWickets: " + wickets + "/" + maxWickets;
            }
        }
        else
        {
            playerScore += playerNumber;
            scoreText.text = "Score: " + playerScore;
            turnText.text = "YOUR TURN\n       BAT";
            NotOut_Image.SetActive(true);
            Wicket_Image.SetActive(false);
        }
    }
    public void ResetGame()
    {
        playerScore = 0;
        wickets = 0;
        scoreText.text = "Score: 0";
        computerText.text = "Computer chose: -";
        turnText.text = "YOUR TURN\n       BAT";
        restartButton.SetActive(false);
        NotOut_Image.SetActive(true);
        Wicket_Image.SetActive(false);
    }
}
