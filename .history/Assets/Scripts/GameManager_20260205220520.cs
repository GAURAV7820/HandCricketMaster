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
    public GameObject tossPanel;   // BAT / BOWL panel

    [Header("Game Settings")]
    public int maxWickets = 5;

    int playerScore = 0;
    int computerScore = 0;
    int wickets = 0;

    bool isFullMatch = false;
    bool playerBatting = true;
    int innings = 1;
    int targetScore = -1;

    void Start()
    {
        int mode = PlayerPrefs.GetInt("GameMode", 0); // 0=BattingOnly, 1=FullMatch
        maxWickets = PlayerPrefs.GetInt("MaxWickets", 5);

        ResetGame();

        if (mode == 0)
        {
            isFullMatch = false;
            turnText.text = "BATTING ONLY\nYOUR TURN TO BAT";
        }
        else
        {
            isFullMatch = true;
            StartToss();
        }
    }

    // ================= INPUT =================
    public void OnNumberPressed(int number)
    {
        if (wickets >= maxWickets)
            return;

        if (!isFullMatch)
        {
            PlayerBat(number);
        }
        else
        {
            if (playerBatting)
                PlayerBat(number);
            else
                PlayerBowl(number);
        }
    }

    // ================= BATTING =================
    void PlayerBat(int playerNumber)
    {
        int computerNumber = Random.Range(0, 7);
        computerText.text = "Computer chose: " + computerNumber;

        if (playerNumber == computerNumber)
        {
            wickets++;
            NotOut_Image.SetActive(false);
            Wicket_Image.SetActive(true);

            if (wickets >= maxWickets)
                EndInnings();
            else
                turnText.text = "OUT!\nWickets: " + wickets + "/" + maxWickets;
        }
        else
        {
            playerScore += playerNumber;
            scoreText.text = "Score: " + playerScore;
            turnText.text = "YOUR TURN\nBAT";

            NotOut_Image.SetActive(true);
            Wicket_Image.SetActive(false);

            if (targetScore > 0 && playerScore >= targetScore)
                EndMatch();
        }
    }

    // ================= BOWLING =================
    void PlayerBowl(int playerBowl)
    {
        int computerBat = Random.Range(0, 7);
        computerText.text = "Computer played: " + computerBat;

        if (playerBowl == computerBat)
        {
            wickets++;
            turnText.text = "WICKET!";

            if (wickets >= maxWickets)
                EndInnings();
        }
        else
        {
            computerScore += computerBat;

            if (targetScore > 0 && computerScore >= targetScore)
                EndMatch();
        }
    }

    // ================= TOSS =================
    void StartToss()
    {
        bool playerWinsToss = Random.Range(0, 2) == 0;

        if (playerWinsToss)
        {
            turnText.text = "YOU WON THE TOSS\nCHOOSE BAT OR BOWL";
            tossPanel.SetActive(true);
        }
        else
        {
            playerBatting = false;
            turnText.text = "COMPUTER WON TOSS\nCOMPUTER BATS FIRST";
            Invoke(nameof(StartInnings), 2f);
        }
    }

    public void ChooseBat()
    {
        playerBatting = true;
        tossPanel.SetActive(false);
        StartInnings();
    }

    public void ChooseBowl()
    {
        playerBatting = false;
        tossPanel.SetActive(false);
        StartInnings();
    }

    // ================= INNINGS =================
    void StartInnings()
    {
        wickets = 0;

        if (playerBatting)
            turnText.text = "INNINGS " + innings + "\nYOU BAT";
        else
            turnText.text = "INNINGS " + innings + "\nYOU BOWL";
    }

    void EndInnings()
    {
        if (innings == 1)
        {
            innings = 2;
            targetScore = playerBatting ? playerScore + 1 : computerScore + 1;
            playerBatting = !playerBatting;
            wickets = 0;
            StartInnings();
        }
        else
        {
            EndMatch();
        }
    }

    // ================= RESULT =================
    void EndMatch()
    {
        if (playerScore > computerScore)
            turnText.text = "YOU WIN!";
        else if (playerScore < computerScore)
            turnText.text = "COMPUTER WINS!";
        else
            turnText.text = "MATCH TIED!";

        restartButton.SetActive(true);
    }

    // ================= RESET =================
    public void ResetGame()
    {
        playerScore = 0;
        computerScore = 0;
        wickets = 0;
        innings = 1;
        targetScore = -1;

        scoreText.text = "Score: 0";
        computerText.text = "Computer chose: -";
        restartButton.SetActive(false);

        NotOut_Image.SetActive(true);
        Wicket_Image.SetActive(false);

        if (tossPanel != null)
            tossPanel.SetActive(false);
    }
}
