using UnityEngine;
using UnityEngine.UI; // Required for the Slider component

public class PlayerHealthUI : MonoBehaviour
{
    [Header("UI Slider Component")]
    [Tooltip("Drag the corner screen Health Bar Slider here.")]
    [SerializeField] private Slider healthSlider;

    private Health playerHealth;
    private int maxHealth;

    private void Start()
    {
        // Automatically find the player ship using its Tag
        GameObject playerShip = GameObject.FindWithTag("Player");

        if (playerShip != null)
        {
            playerHealth = playerShip.GetComponent<Health>();

            if (playerHealth != null)
            {
                // Capture the starting health as the Maximum Health value
                maxHealth = playerHealth.ObjectHealth;

                // Configure the UI slider constraints
                if (healthSlider != null)
                {
                    healthSlider.maxValue = maxHealth;
                    healthSlider.value = maxHealth;
                }
            }
            else
            {
                Debug.LogError("[PlayerHealthUI] The Player GameObject is missing a Health component!");
            }
        }
        else
        {
            Debug.LogError("[PlayerHealthUI] Could not find a GameObject tagged 'Player' in the scene!");
        }
    }

    private void Update()
    {
        // Continuously sync the slider value with the player's current health remaining
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.value = playerHealth.ObjectHealth;
        }
    }
}
