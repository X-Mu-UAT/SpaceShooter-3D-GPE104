using UnityEngine;
using TMPro; 

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
        }
    }

    private void Start()
    {
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
