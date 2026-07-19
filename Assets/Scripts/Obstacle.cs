using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Config
    public int CrashDamage = 25;
    public LayerMask TargetLayers;

    void OnTriggerEnter(Collider other)
    {
        ProcessImpact(other.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        ProcessImpact(collision.gameObject);
    }

    private void ProcessImpact(GameObject hitObject)
    {
        // Check if the object we rammed into belongs to the Player layer
        if (((1 << hitObject.layer) & TargetLayers) != 0)
        {
            // Reach into the player object and grab their health script
            Health playerHealth = hitObject.GetComponent<Health>();

            if (playerHealth != null)
            {
                // Inflict crash damage directly to the ship
                playerHealth.TakeDamage(CrashDamage);
                Debug.Log($"[Hazard] Player crashed into {gameObject.name}! Dealt {CrashDamage} damage.");
            }
        }
    }
}