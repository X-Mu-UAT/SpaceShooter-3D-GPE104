using UnityEngine;

public class AstronautPickup : MonoBehaviour
{
    [Header("Score Settings")]
    [Tooltip("Points awarded for rescuing this astronaut.")]
    [SerializeField] private int scoreValue = 500;

    [Header("Visual Effects")]
    [SerializeField] private GameObject rescueParticlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Verify that the object touching the astronaut is the human player
        // We look for your custom SpaceshipController script to confirm it's the player
        if (other.GetComponent<Pawn>() != null)
        {
            // 2. Play rescue audio via your AudioManager instance
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(SFXType.AstronautPickup);
            }

            // 3. FIX: Add the rescue points directly to your persistent GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddToScore(scoreValue);
                Debug.Log($"Astronaut rescued! Score +{scoreValue}. Total: {GameManager.Instance.GetCurrentScore()}");
            }
            else
            {
                Debug.LogError("[AstronautPickup] Could not find GameManager.Instance! Score not counted.");
            }

            // 4. Optional: Spawn a glittering visual particle burst on rescue
            if (rescueParticlePrefab != null)
            {
                Instantiate(rescueParticlePrefab, transform.position, transform.rotation);
            }

            // 5. Delete the astronaut from the space scene so they can only be grabbed once
            Destroy(gameObject);
        }
    }
}
