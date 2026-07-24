using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameOverScreen : MonoBehaviour
{
    // ADDED: This public static variable fixes the compiler error!
    public static bool PlayerWon = false;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Dynamic Text Configurations")]
    [Tooltip("Drag your Title Text object here (The one that says Game Over or Victory).")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private string winText = "VICTORY!";
    [SerializeField] private string loseText = "GAME OVER";

    [Header("Scene Configuration")]
    [Tooltip("Type the exact name of your Main Menu scene here.")]
    [SerializeField] private string mainMenuSceneName = "MainMenuScene";

    void Start()
    {
        // Automatically swap the text based on whether the player won or lost
        if (titleText != null)
        {
            titleText.text = PlayerWon ? winText : loseText;
        }

        // Ensure the screen starts in the correct state 
        ShowGameOverScreen();
    }

    // Call this to flip from Game Over to the Credits screen 
    public void OpenCredits()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
    }

    // Call this to flip from Credits back to the Game Over screen 
    public void ShowGameOverScreen()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    // Optional: Call this from a restart button to reload your flight level 
    public void RestartGame()
    {
        // Reset the win flag before restarting gameplay
        PlayerWon = false;

        // Reloads whichever scene is currently active (your gameplay scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Call this from your Main Menu UI Button 
    public void LoadMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Debug.LogError("[GameOverScreen] Main Menu scene name is empty!");
        }
    }

    // NEW: Call this from your Quit UI Button 
    public void QuitGame()
    {
        Debug.Log("Player closed the game window.");

        // Closes the standalone built application 
        Application.Quit();

#if UNITY_EDITOR
        // Stops execution if you are testing inside the Unity editor itself 
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
