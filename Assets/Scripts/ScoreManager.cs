using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Added to monitor level changes

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI Reference")]
    [Tooltip("Optional: Drag your UI TextMeshPro component here to display the score.")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore = 0;

    private void Awake()
    {
        // Enforce the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps score alive across level loads
        }
        else
        {
            Destroy(gameObject);
            return; // Exit immediately so we don't execute subscription logic below
        }
    }

    private void OnEnable()
    {
        // Listen for when a new scene/level is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Clean up the listener if the manager is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This runs automatically whenever a new level opens
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If the reference was lost during the level change, find the new one automatically
        if (scoreText == null)
        {
            FindNewScoreTextUI();
        }
        else
        {
            UpdateScoreUI();
        }
    }

    private void Start()
    {
        if (scoreText == null)
        {
            FindNewScoreTextUI();
        }
        else
        {
            UpdateScoreUI();
        }
    }

    // Searches the newly loaded scene for a TextMeshPro component attached to an object named "ScoreText"
    private void FindNewScoreTextUI()
    {
        GameObject uiObject = GameObject.Find("ScoreText");
        if (uiObject != null)
        {
            scoreText = uiObject.GetComponent<TextMeshProUGUI>();
        }

        UpdateScoreUI();
    }

    // Call this from other scripts to add points
    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log($"Score updated! Current score: {currentScore}");
        UpdateScoreUI();
    }

    // Returns the current score value if needed by other systems
    public int GetCurrentScore()
    {
        return currentScore;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }
}
