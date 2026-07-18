using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] private int damageAmount = 25;

    private void OnCollisionEnter(Collision collision)
    {
        // The bullet will ONLY trigger if the thing it hits has a Health script
        if (collision.gameObject.TryGetComponent(out IDamageable damageableTarget))
        {
            damageableTarget.TakeDamage(damageAmount);
            Destroy(gameObject); // Destroy bullet on impact
        }
        else
        {
            // Optional: Destroy the bullet if it hits a wall/floor without dealing damage
            // Destroy(gameObject); 
        }
    }
}
