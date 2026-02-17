using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Text")]
    public TMP_Text turnText;
    public TMP_Text computerText;
    public TMP_Text liveScoreText;
    public TMP_Text targetText;

    [Header("UI Objects")]
    public GameObject restartButton;
    public GameObject NotOut_Image;
    public GameObject Wicket_Image;
    public GameObject gameplayUI;

    [Header("Popup UI")]
    public GameObject background;
    public GameObject popupPanel;
    public TMP_Text popupText;

    public GameObject tossChoicePanel;
    public GameObject tossPanel;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip tapSound;
    public AudioClip BatHitting;
    public AudioClip wicketsound;
    public AudioClip MissBall;
    public AudioClip Gamewin;
    public AudioClip Sixsound;
    public AudioClip Gameloose;

    [Header("Game Settings")]
    public int maxWickets = 5;

    int playerScore = 0;
    int computerScore = 0;
    int wickets = 0;
    int highScore = 0;

    bool isFullMatch = false;
    bool playerBatting = true;
    bool playerChoseHeads;
    bool isQuitPopup = false;

    int innings = 1;
    int targetScore = -1;

    // ================= START =================
    void Start()
    {
        int mode = PlayerPrefs.GetInt("GameMode", 0);
        maxWickets = PlayerPrefs.GetInt("MaxWickets", 5);
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        ResetGame();

        if (PlayerPrefs.GetInt("HintShown", 0) == 0)
        {
            ShowPopup("HINT\nIf both choose the same number → OUT");
            PlayerPrefs.SetInt("HintShown", 1);
            PlayerPrefs.Save();
        }

        if (mode == 0)
        {
            isFullMatch = false;
            playerBatting = true;
            turnText.text = "BATTING ONLY\nYOUR TURN TO BAT";
            UpdateLiveScore();
            UpdateTargetText();
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
        if (popupPanel.activeSelf) return;
        if (wickets >= maxWickets) return;

        // SOUND
        if (number == 0)
            audioSource.PlayOneShot(MissBall);
        else if (number >= 4)
            audioSource.PlayOneShot(Sixsound);
        else
            audioSource.PlayOneShot(BatHitting);

        if (!isFullMatch)
            PlayerBat(number);
        else
        {
            if (playerBatting) PlayerBat(number);
            else PlayerBowl(number);
        }
    }

    // ================= TOSS =================
    void StartToss()
    {
        gameplayUI.SetActive(false);
        tossChoicePanel.SetActive(true);
        turnText.text = "CHOOSE HEADS OR TAILS";
    }

    public void ChooseHeads() { playerChoseHeads = true; ResolveToss(); }
    public void ChooseTails() { playerChoseHeads = false; ResolveToss(); }

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
            playerBatting = false;
            turnText.text = "YOU LOST THE TOSS\nCOMPUTER BATS FIRST";
            gameplayUI.SetActive(true);
            Invoke(nameof(StartInnings), 1.5f);
        }
    }

    public void ChooseBat()
    {
        playerBatting = true;
        tossPanel.SetActive(false);
        gameplayUI.SetActive(true);
        StartInnings();
    }

    public void ChooseBowl()
    {
        playerBatting = false;
        tossPanel.SetActive(false);
        gameplayUI.SetActive(true);
        StartInnings();
    }

    // ================= GAMEPLAY =================
    void PlayerBat(int playerNumber)
    {
        int computerNumber = Random.Range(1, 7);
        computerText.text = "COMPUTER BOWLED: " + computerNumber;

        if (playerNumber == computerNumber)
        {
            wickets++;

#if UNITY_ANDROID
            Handheld.Vibrate();
#endif

            audioSource.Stop();
            audioSource.PlayOneShot(wicketsound);

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
            turnText.text = "YOU BAT";

            if (isFullMatch && targetScore > 0 && playerScore >= targetScore)
                EndMatch();
        }

        UpdateLiveScore();
        UpdateTargetText();
    }

    void PlayerBowl(int playerBowl)
    {
        int computerBat = Random.Range(1, 7);
        computerText.text = "COMPUTER BATTED: " + computerBat;

        if (playerBowl == computerBat)
        {
            wickets++;

#if UNITY_ANDROID
            Handheld.Vibrate();
#endif

            audioSource.PlayOneShot(wicketsound);

            Wicket_Image.SetActive(true);
            NotOut_Image.SetActive(false);
            turnText.text = "WICKET!";

            if (wickets >= maxWickets)
                EndInnings();
        }
        else
        {
            computerScore += computerBat;
            Wicket_Image.SetActive(false);
            NotOut_Image.SetActive(true);
            turnText.text = "YOU BOWL";

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
        Wicket_Image.SetActive(false);
        NotOut_Image.SetActive(true);

        turnText.text = playerBatting ?
            "INNINGS " + innings + "\nYOU BAT" :
            "INNINGS " + innings + "\nYOU BOWL";

        UpdateLiveScore();
        UpdateTargetText();
    }

    void EndInnings()
    {
        if (!isFullMatch)
        {
            EndMatch();
            return;
        }

        if (innings == 1)
        {
            innings = 2;
            targetScore = playerBatting ? playerScore + 1 : computerScore + 1;
            playerBatting = !playerBatting;

            ShowPopup("FIRST INNINGS OVER\nTARGET: " + targetScore + "\nGOOD LUCK!");
        }
        else
        {
            EndMatch();
        }
    }

    // ================= RESULT =================
    void EndMatch()
    {
#if UNITY_ANDROID
        Handheld.Vibrate();
#endif

        if (!isFullMatch)
        {
            if (playerScore > highScore)
            {
                highScore = playerScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
            }

            audioSource.PlayOneShot(Gamewin);

            ShowPopup("GAME OVER\nSCORE: " + playerScore +
                      "\nHIGH SCORE: " + highScore);
        }
        else
        {
            string resultText;

            if (playerScore > computerScore)
            {
                audioSource.PlayOneShot(Gamewin);
                resultText = "YOU WON!!";
            }
            else if (playerScore < computerScore)
            {
                audioSource.PlayOneShot(Gameloose);
                resultText = "YOU LOSE!!";
            }
            else
            {
                resultText = "MATCH TIED!!";
            }

            ShowPopup("MATCH SUMMARY\nYOU: " + playerScore +
                      "\nCOMPUTER: " + computerScore +
                      "\n" + resultText);
        }

        restartButton.SetActive(true);
    }

    // ================= POPUP =================
    void ShowPopup(string message)
    {
        popupText.text = message;
        background.SetActive(true);
        popupPanel.SetActive(true);
    }

    public void OnPopupOK()
    {
        background.SetActive(false);
        popupPanel.SetActive(false);

        // RESET quit flag
        isQuitPopup = false;

        if (isFullMatch && innings == 2 && wickets == 0)
            StartInnings();
    }

    // ================= BACK BUTTON =================
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            HandleBackButton();
    }

    void HandleBackButton()
    {
        // QUIT popup already open → EXIT
        if (popupPanel.activeSelf && isQuitPopup)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        // Normal popup open → close only
        if (popupPanel.activeSelf)
        {
            background.SetActive(false);
            popupPanel.SetActive(false);
            return;
        }

        // Match ended → go menu
        if (restartButton.activeSelf)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        // Show quit popup
        isQuitPopup = true;
        ShowPopup("QUIT MATCH?\nProgress will be lost.");
    }

    // ================= SCORE =================
    void UpdateLiveScore()
    {
        if (!isFullMatch)
            liveScoreText.text = "SCORE: " + playerScore + " / " + wickets;
        else
            liveScoreText.text = playerBatting ?
                "YOU: " + playerScore + " / " + wickets :
                "COMPUTER: " + computerScore + " / " + wickets;
    }

    void UpdateTargetText()
    {
        if (!isFullMatch)
            targetText.text = "HIGH SCORE: " + highScore;
        else
            targetText.text = innings == 2 ? "TARGET: " + targetScore : "";
    }

    // ================= RESET =================
    public void ResetGame()
    {
        playerScore = 0;
        computerScore = 0;
        wickets = 0;
        innings = 1;
        targetScore = -1;
        isQuitPopup = false;

        restartButton.SetActive(false);
        background.SetActive(false);
        popupPanel.SetActive(false);
        tossPanel.SetActive(false);
        tossChoicePanel.SetActive(false);
    }
}
