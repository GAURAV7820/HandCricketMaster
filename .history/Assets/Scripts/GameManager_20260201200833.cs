using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText;
    public TMP_Text scoreText;
    public TMP_Text computerText;
    public GameObject restartButton;

    int playerScore = 0;
    bool isOut = false;

    void Start()
    {
        ResetGame();
    }

    public void OnNumberPressed(int playerNumber)
    {
        if (isOut) return;

        int computerNumber = Random.Range(0, 7);
        computerText.text = "Computer chose: " + computerNumber;

        if (playerNumber == computerNumber)
        {
            isOut = true;
            turnText.text = "OUT! Final Score: " + playerScore;
            restartButton.SetActive(true);
        }
        else
        {
            playerScore += playerNumber;
            scoreText.text = "Score: " + playerScore;
            turnText.text = "YOUR TURN\n       BAT";
        }
    }

    public void ResetGame()
    {
        playerScore = 0;
        isOut = false;

        turnText.text = "YOUR TURN\n       BAT";
        scoreText.text = "Score: 0";
        computerText.text = "Computer chose: -";
        restartButton.SetActive(false);
    }
}