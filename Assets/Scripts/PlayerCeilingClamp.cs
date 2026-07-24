using UnityEngine;

public class PlayerCeilingClamp : MonoBehaviour
{
    private Level currentLevel;
    private Rigidbody rb; // Add reference to the spaceship's Rigidbody

    private void Start()
    {
        // Cache the Rigidbody component from the spaceship
        rb = GetComponent<Rigidbody>();

        // Safe, non-obsolete lookup for the unique level instance
        currentLevel = Object.FindAnyObjectByType<Level>();
    }

    private void LateUpdate()
    {
        if (currentLevel == null) return;

        // Teleport the ship instantly downward if it breaches the designer-defined Y height
        if (transform.position.y > currentLevel.YMovementLimit)
        {
            // Snap the position to the ceiling
            transform.position = new Vector3(transform.position.x, currentLevel.YMovementLimit, transform.position.z);

            // CRITICAL: Stop the upward physical physics momentum to prevent jittering
            if (rb != null)
            {
                Vector3 currentVelocity = rb.linearVelocity;

                // Only kill the velocity if it's pointing upward
                if (currentVelocity.y > 0f)
                {
                    currentVelocity.y = 0f;
                    rb.linearVelocity = currentVelocity;
                }
            }
        }
    }
}
