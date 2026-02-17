using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Credits")]
    [TextArea(4, 12)]
    public string creditsText =
        "HAND CRICKET\n" +
        "\n" +
        "Design & Development: Your Name\n" +
        "Art: Your Name\n" +
        "Music/SFX: Your Name\n" +
        "\n" +
        "Special Thanks: Everyone who played!";

    private GameObject creditsPanel;
    private TMP_Text creditsBody;

    public void PlayGame()
    {
        SceneManager.LoadScene("ModeSelection");
    }

    public void OpenCredits()
    {
        EnsureCreditsPanel();
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited");
    }

    private void CloseCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
        }
    }

    private void EnsureCreditsPanel()
    {
        if (creditsPanel != null)
        {
            if (creditsBody != null)
            {
                creditsBody.text = creditsText;
            }
            return;
        }

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogWarning("Credits panel could not be created: no Canvas found in scene.");
            return;
        }

        creditsPanel = new GameObject("CreditsPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        creditsPanel.transform.SetParent(canvas.transform, false);
        RectTransform panelRect = creditsPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;
        Image panelImage = creditsPanel.GetComponent<Image>();
        panelImage.color = new Color(0f, 0f, 0f, 0.85f);

        GameObject content = new GameObject("CreditsContent", typeof(RectTransform));
        content.transform.SetParent(creditsPanel.transform, false);
        RectTransform contentRect = content.GetComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0.1f, 0.2f);
        contentRect.anchorMax = new Vector2(0.9f, 0.85f);
        contentRect.offsetMin = Vector2.zero;
        contentRect.offsetMax = Vector2.zero;

        GameObject textObj = new GameObject("CreditsText", typeof(RectTransform), typeof(TextMeshProUGUI));
        textObj.transform.SetParent(content.transform, false);
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        creditsBody = textObj.GetComponent<TextMeshProUGUI>();
        creditsBody.text = creditsText;
        creditsBody.alignment = TextAlignmentOptions.Center;
        creditsBody.fontSize = 36;
        creditsBody.enableWordWrapping = true;

        GameObject buttonObj = new GameObject("CreditsCloseButton", typeof(RectTransform), typeof(Image), typeof(Button));
        buttonObj.transform.SetParent(creditsPanel.transform, false);
        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.5f, 0.05f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.05f);
        buttonRect.sizeDelta = new Vector2(260f, 80f);
        buttonRect.anchoredPosition = Vector2.zero;

        Image buttonImage = buttonObj.GetComponent<Image>();
        buttonImage.color = new Color(1f, 1f, 1f, 0.9f);

        Button button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(CloseCredits);

        GameObject buttonTextObj = new GameObject("ButtonText", typeof(RectTransform), typeof(TextMeshProUGUI));
        buttonTextObj.transform.SetParent(buttonObj.transform, false);
        RectTransform buttonTextRect = buttonTextObj.GetComponent<RectTransform>();
        buttonTextRect.anchorMin = Vector2.zero;
        buttonTextRect.anchorMax = Vector2.one;
        buttonTextRect.offsetMin = Vector2.zero;
        buttonTextRect.offsetMax = Vector2.zero;

        TextMeshProUGUI buttonText = buttonTextObj.GetComponent<TextMeshProUGUI>();
        buttonText.text = "Back";
        buttonText.alignment = TextAlignmentOptions.Center;
        buttonText.fontSize = 36;
        buttonText.color = Color.black;

        creditsPanel.SetActive(false);
    }
}
