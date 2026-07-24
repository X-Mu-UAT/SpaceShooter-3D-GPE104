using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Win Settings")]
    [Tooltip("Set this exactly to your GameOverScene name.")]
    [SerializeField] private string gameOverSceneName = "GameOverScene";

    private int activeEnemyCount = 0;

    private void Awake()
    {
        // Enforce the singleton pattern immediately on frame zero
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called automatically by each UFO's Health script during its Start() phase
    public void RegisterEnemy()
    {
        activeEnemyCount++;
        Debug.Log($"Enemy registered! Remaining targets: {activeEnemyCount}");
    }

    // Called automatically when a UFO takes fatal damage
    public void UnregisterEnemy()
    {
        activeEnemyCount--;
        Debug.Log($"Enemy defeated! Remaining targets: {activeEnemyCount}");

        if (activeEnemyCount <= 0)
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        // Tell the game over screen that the player actually won!
        GameOverScreen.PlayerWon = true;

        // Unlock mouse cursor so player can click buttons on the win screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Load the shared end screen
        SceneManager.LoadScene(gameOverSceneName);
    }
}
