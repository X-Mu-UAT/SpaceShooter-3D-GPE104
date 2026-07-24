using UnityEngine;
using UnityEngine.SceneManagement; // CRITICAL: Added so we can load the Game Over scene

public class Health : MonoBehaviour
{
    public int ObjectHealth = 100;

    [Header("Score Configuration")]
    [Tooltip("Check this if you want the player to get points when this object dies.")]
    [SerializeField] private bool awardsScoreOnDeath = true;
    [SerializeField] private int scoreValue = 250;

    [Header("Player Tracking")]
    [Tooltip("Check this box ONLY if this script is attached to the player's spaceship.")]
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private string gameOverSceneName = "GameOverScene";

    private Death death;
    private SpaceshipRespawn respawnScript;

    void Start()
    {
        death = GetComponent<Death>();
        respawnScript = GetComponent<SpaceshipRespawn>();

        // If this is an enemy (NOT the player), register it automatically with the LevelManager
        if (!isPlayer && LevelManager.Instance != null)
        {
            LevelManager.Instance.RegisterEnemy();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        // Prevent damage calculation if the ship is already "dead"
        if (ObjectHealth <= 0) return;

        ObjectHealth -= damage;

        if (ObjectHealth <= 0)
        {
            // Trigger the score right before death execution 
            if (awardsScoreOnDeath && ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }

            // PRIORITY 1: Check if this is the player. If so, ignore respawns and load Game Over!
            if (isPlayer)
            {
                GameOverScreen.PlayerWon = false; // Mark as a loss

                // Unlock the mouse cursor so you can actually click the menu buttons
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                SceneManager.LoadScene(gameOverSceneName);
                return; // Stop execution here so the ship doesn't try to run respawn/death components below
            }

            // PRIORITY 2: If it's a non-player object that tracks enemy limits, unregister it
            if (!isPlayer && LevelManager.Instance != null)
            {
                LevelManager.Instance.UnregisterEnemy();
            }

            // PRIORITY 3: If it's an object with a respawn system, use it
            if (respawnScript != null)
            {
                respawnScript.TriggerRespawn();
            }
            // PRIORITY 4: Fallback to permanent destruction for simple hazards/enemies
            else if (death != null)
            {
                death.DoDeath();
            }
        }
    }
}
