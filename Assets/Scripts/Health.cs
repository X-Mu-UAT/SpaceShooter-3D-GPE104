using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int ObjectHealth = 500;

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

        // If this is an enemy UFO, register it with the LevelManager
        if (!isPlayer && LevelManager.Instance != null)
        {
            LevelManager.Instance.RegisterEnemy();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (ObjectHealth <= 0) return;

        ObjectHealth -= damage;

        if (ObjectHealth <= 0)
        {
            // FIX 1: ALWAYS payout scores to the GameManager first, regardless of who died!
            if (awardsScoreOnDeath && GameManager.Instance != null)
            {
                GameManager.Instance.AddToScore(scoreValue);
            }

            // PRIORITY 1: Handle Player Death
            if (isPlayer)
            {
                if (GameManager.Instance != null)
                {
                    // LoseALife handles respawning. If it returns TRUE, player is permanently out of lives.
                    bool isGameOver = GameManager.Instance.LoseALife(gameObject, respawnScript);

                    if (isGameOver)
                    {
                        GameOverScreen.PlayerWon = false; // Mark loss

                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;

                        SceneManager.LoadScene(gameOverSceneName);
                    }
                }
                return; // Blocks execution so player doesn't fall through to enemy destruction logic
            }

            // PRIORITY 2: Handle Enemy UFO Death
            if (!isPlayer && LevelManager.Instance != null)
            {
                LevelManager.Instance.UnregisterEnemy();
            }

            if (respawnScript != null)
            {
                respawnScript.TriggerRespawn();
            }
            else if (death != null)
            {
                death.DoDeath();
            }
        }
    }
}
