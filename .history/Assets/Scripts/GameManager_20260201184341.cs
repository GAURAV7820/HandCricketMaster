using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText;
    public TMP_Text scoreText;

    bool isPlayer1Batting = true;
    int currentBatNumber;
    int player1Score = 0;

    void Start()
    {
        UpdateUI();
    }

    public void OnNumberPressed(int number)
    {
        if (isPlayer1Batting)
        {
            currentBatNumber = number;
            turnText.text = "PASS PHONE TO BOWLER";
            isPlayer1Batting = false;
        }
        else
        {
            if (currentBatNumber == number)
            {
                turnText.text = "OUT!";
            }
            else
            {
                player1Score += currentBatNumber;
                scoreText.text = "Score: " + player1Score;
                turnText.text = "PLAYER 1 BATTING";
            }

            isPlayer1Batting = true;
        }
    }

    void UpdateUI()
    {
        turnText.text = "PLAYER 1 BATTING";
        scoreText.text = "Score: 0";
    }
}