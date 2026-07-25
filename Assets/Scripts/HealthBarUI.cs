using UnityEngine;
using UnityEngine.UI; // CRITICAL: Gives access to the UI Image component

public class HealthBarUI : MonoBehaviour
{
    private Image fillImage;
    private Health playerHealth;

    [Header("Max Health Settings")]
    [Tooltip("Must match your player's maximum health pool exactly.")]
    [SerializeField] private float maxHealth = 500f;

    void Awake()
    {
        // Cache the filled Image component attached to this object
        fillImage = GetComponent<Image>();
    }

    void Start()
    {
        // Find all Health components in the scene
        Health[] allHealthScripts = Object.FindObjectsByType<Health>(FindObjectsInactive.Include);

        foreach (Health h in allHealthScripts)
        {
            // Lock onto the script that has the 'Is Player' box ticked ON
            if (h.IsPlayer)
            {
                playerHealth = h;
                break;
            }
        }

        if (playerHealth == null)
        {
            Debug.LogWarning("[HealthBarUI] Could not find any active GameObject with a Health script marked 'Is Player'.");
        }
    }

    void Update()
    {
        if (playerHealth != null && fillImage != null)
        {
            // Calculate current health percentage
            // NOTE: We cast ObjectHealth to a float so the division math doesn't truncate to 0
            float healthPercentage = (float)playerHealth.ObjectHealth / maxHealth;

            // Clamp the value strictly between 0 and 1 to prevent UI graphic overflow bugs
            fillImage.fillAmount = Mathf.Clamp01(healthPercentage);

            // Dynamic color shift: Automatically blends from Green to Red as health drops
            fillImage.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        }
    }
}
