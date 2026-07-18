using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameManager()
    {
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // CORRECT & UPDATED: Uses the faster, non-sorting method
                _instance = Object.FindAnyObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to the death event when the game manager turns on
        PlayerHealth.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks when changing scenes
        PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("GameManager received player death event. Triggering Game Over screen...");

        // Call your reload scene or Game Over UI logic here
        // Invoke(nameof(RestartScene), 2f); 
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
