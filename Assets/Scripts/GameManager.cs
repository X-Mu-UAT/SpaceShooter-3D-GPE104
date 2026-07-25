using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Lives Configuration")]
    [SerializeField] private int playerLives = 3;

    private int currentScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps score/lives alive between scene swaps
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetRemainingLives()
    {
        return playerLives;
    }

    public void AddToScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score updated! Current Score: {currentScore}");
    }

    public int GetCurrentScore() => currentScore;

    public bool LoseALife(GameObject playerShip, SpaceshipRespawn respawnSystem)
    {
        playerLives--;
        Debug.Log($"Player lost a life! Lives remaining: {playerLives}");

        if (playerLives <= 0)
        {
            SaveHighScore();
            return true; // Game Over
        }

        // Safely teleport the player's physical body and visual transform to (0,0,0)
        if (playerShip != null)
        {
            Rigidbody playerRigidbody = playerShip.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // FIXED: Manually zero out all velocity values to stop the physics engine cleanly.
                // This replaces ResetDynamics() and works on every single version of Unity.
                playerRigidbody.linearVelocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;

                // Snap the physical physics container directly to the origin
                playerRigidbody.position = Vector3.zero;
                playerRigidbody.rotation = Quaternion.identity;
            }

            // Snap the visual hierarchy transform to match (0,0,0)
            playerShip.transform.position = Vector3.zero;
            playerShip.transform.rotation = Quaternion.identity;
        }

        // Reset player health if component exists
        Health playerHealthComponent = playerShip != null ? playerShip.GetComponent<Health>() : null;
        if (playerHealthComponent != null)
        {
            playerHealthComponent.ObjectHealth = 500;
        }

        if (respawnSystem != null)
        {
            respawnSystem.TriggerRespawn();
        }

        return false; // Not Game Over
    }

    public void SaveHighScorePublic()
    {
        SaveHighScore();
    }

    private void SaveHighScore()
    {
        int savedHighScore = PlayerPrefs.GetInt("SpaceHighScore", 0);

        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("SpaceHighScore", currentScore);
            PlayerPrefs.Save();
            Debug.Log($"New High Score Saved: {currentScore}");
        }
    }

    public void ResetGameData()
    {
        currentScore = 0;
        playerLives = 3;
    }
}
