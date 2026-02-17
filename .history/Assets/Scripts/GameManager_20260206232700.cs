using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Text")]
    public TMP_Text turnText;
    public TMP_Text scoreText;      // optional
    public TMP_Text computerText;
    public TMP_Text liveScoreText;  // shows 24/3
    public TMP_Text targetText;     // shows TARGET

    [Header("UI Objects")]
    public GameObject restartButton;
    public GameObject NotOut_Image;
    public GameObject Wicket_Image;
    public GameObject gameplayUI;

    public GameObject tossChoicePanel; // HEADS / TAILS
    public GameObject tossPanel;       // BAT / BOWL

    [Header("Game Settings")]
    public int maxWickets = 5;

    int playerScore = 0;
    int computerScore = 0;
    int wickets = 0;

    bool isFullMatch = false;
    bool playerBatting = true;
    bool playerChoseHeads;

    int innings = 1;
    int targetScore = -1;

    // ================= START =================
    void Start()
    {
        int mode = PlayerPrefs.GetInt("GameMode", 0);
        maxWickets = PlayerPrefs.GetInt("MaxWickets", 5);

        ResetGame();

        if (mode == 0)
        {
            isFullMatch = false;
            turnText.text = "BATTING ONLY\nYOUR TURN TO BAT";
            UpdateLiveScore();
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

    // ================= TOSS =================
    void StartToss()
    {
        gameplayUI.SetActive(false);
        turnText.text = "CHOOSE HEADS OR TAILS";
        tossChoicePanel.SetActive(true);
    }

    public void ChooseHeads()
    {
        playerChoseHeads = true;
        ResolveToss();
    }

    public void ChooseTails()
    {
        playerChoseHeads = false;
        ResolveToss();
    }

    void ResolveToss()
    {
        tossChoicePanel.SetActive(false);

        bool tossResultIsHeads = Random.Range(0, 2) == 0;
        bool playerWinsToss = (playerChoseHeads == tossResultIsHeads);

        if (playerWinsToss)
        {
            turnText.text = "YOU WON THE TOSS\nCHOOSE BAT OR BOWL";
            tossPanel.SetActive(true);
        }
        else
        {
            gameplayUI.SetActive(true);
            playerBatting = false;
            turnText.text = "YOU LOST THE TOSS\nCOMPUTER BATS FIRST";
            Invoke(nameof(StartInnings), 2f);
        }
    }

    public void ChooseBat()
    {
        gameplayUI.SetActive(true);
        playerBatting = true;
        tossPanel.SetActive(false);
        StartInnings();
    }

    public void ChooseBowl()
    {
        gameplayUI.SetActive(true);
        playerBatting = false;
        tossPanel.SetActive(false);
        StartInnings();
    }

    // ================= GAMEPLAY =================
    void PlayerBat(int playerNumber)
    {
        int computerNumber = Random.Range(0, 7);
        computerText.text = "COMPUTER BOWLED: " + computerNumber;

        if (playerNumber == computerNumber)
        {
            wickets++;
            Wicket_Image.SetActive(true);
            NotOut_Image.SetActive(false);
            turnText.text = "OUT!";

            if (wickets >= maxWickets)
                EndInnings();
        }
        else
        {
            playerScore += playerNumber;
            Wicket_Image.SetActive(false);
            NotOut_Image.SetActive(true);
            turnText.text = "YOUR TURN\nBAT";

            if (targetScore > 0 && playerScore >= targetScore)
                EndMatch();
        }

        UpdateLiveScore();
        UpdateTargetText();
    }

    void PlayerBowl(int playerBowl)
    {
        int computerBat = Random.Range(0, 7);
        computerText.text = "COMPUTER BATTED: " + computerBat;

        if (playerBowl == computerBat)
        {
            wickets++;
            Wicket_Image.SetActive(true);     // ✅ FIX
            NotOut_Image.SetActive(false);    // ✅ FIX
            turnText.text = "WICKET!";

            if (wickets >= maxWickets)
                EndInnings();
        }
        else
        {
            computerScore += computerBat;

            Wicket_Image.SetActive(false);    // ✅ FIX
            NotOut_Image.SetActive(true);     // ✅ FIX

            if (targetScore > 0 && computerScore >= targetScore)
                EndMatch();
        }

        UpdateLiveScore();
        UpdateTargetText();
    }

    // ================= INNINGS =================
    void StartInnings()
    {
        wickets = 0;

        // Reset images at start of innings (SAFETY)
        Wicket_Image.SetActive(false);
        NotOut_Image.SetActive(true);

        if (playerBatting)
            turnText.text = "INNINGS " + innings + "\nYOU BAT";
        else
            turnText.text = "INNINGS " + innings + "\nYOU BOWL";

        UpdateLiveScore();
        UpdateTargetText();
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

    // ================= SCOREBOARD =================
    void UpdateLiveScore()
    {
        if (playerBatting)
            liveScoreText.text = "YOU: " + playerScore + " / " + wickets;
        else
            liveScoreText.text = "COMPUTER: " + computerScore + " / " + wickets;
    }

    void UpdateTargetText()
    {
        if (innings == 2)
            targetText.text = "TARGET: " + targetScore;
        else
            targetText.text = "";
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

        liveScoreText.text = "";
        targetText.text = "";
        computerText.text = "Computer chose: -";

        restartButton.SetActive(false);
        NotOut_Image.SetActive(true);
        Wicket_Image.SetActive(false);

        if (tossPanel != null) tossPanel.SetActive(false);
        if (tossChoicePanel != null) tossChoicePanel.SetActive(false);
    }
}
