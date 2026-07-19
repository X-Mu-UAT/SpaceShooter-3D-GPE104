using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [Tooltip("Amount of damage this obstacle deals to the player.")]
    [SerializeField] private int damageAmount = 10;

    // Triggers when the player physically impacts a solid obstacle (e.g., hitting a wall)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health playerHealth))
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }

    // Triggers if your obstacle is a "Trigger" zone (e.g., passing through a laser or boundary)
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health playerHealth))
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
