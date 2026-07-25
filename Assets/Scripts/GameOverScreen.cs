using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public static bool PlayerWon = false;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Dynamic Text Configurations")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private string winText = "VICTORY!";
    [SerializeField] private string loseText = "GAME OVER";

    [Header("High Score UI Fields")]
    [Tooltip("Drag a TextMeshPro component here to show the persistent High Score.")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    [Tooltip("Drag a TextMeshPro component here to show the Current Match Score.")]
    [SerializeField] private TextMeshProUGUI currentScoreText;

    [Header("Scene Configuration")]
    [SerializeField] private string mainMenuSceneName = "MainMenuScene";

    void Start()
    {
        if (titleText != null)
        {
            titleText.text = PlayerWon ? winText : loseText;
        }

        // CRITICAL: The string key "SpaceHighScore" must match exactly!
        int savedHighScore = PlayerPrefs.GetInt("SpaceHighScore", 0);

        if (highScoreText != null)
        {
            highScoreText.text = $"HIGH SCORE: {savedHighScore}";
        }

        if (GameManager.Instance != null && currentScoreText != null)
        {
            currentScoreText.text = $"YOUR SCORE: {GameManager.Instance.GetCurrentScore()}";
        }

        ShowGameOverScreen();
    }


    public void OpenCredits()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    public void ShowGameOverScreen()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    public void RestartGame()
    {
        PlayerWon = false;

        // Reset your structural match variables back to 0 score and 3 lives
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameData();
        }

        SceneManager.LoadScene(1); // Loads your core gameplay level scene index
    }

    public void LoadMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
