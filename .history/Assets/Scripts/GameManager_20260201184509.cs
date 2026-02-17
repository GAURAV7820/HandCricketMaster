using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text turnText;
    public TMP_Text scoreText;
    public TMP_Text computerText;

    int playerScore = 0;
    bool isOut = false;

    void Start()
    {
        turnText.text = "YOUR TURN - BAT";
        scoreText.text = "Score: 0";
        computerText.text = "";
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
        }
        else
        {
            playerScore += playerNumber;
            scoreText.text = "Score: " + playerScore;
            turnText.text = "YOUR TURN - BAT";
        }
    }
}