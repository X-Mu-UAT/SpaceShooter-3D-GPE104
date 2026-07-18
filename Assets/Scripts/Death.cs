using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Death Presentation")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private Behaviour[] componentsToDisable; // e.g., PlayerController, Collider

    // Modern C# Action: Any system (UI, Audio, GameManager) can listen to this
    public static event Action OnPlayerDeath;

    // Optional Unity Event for designer flexibility in the inspector
    public UnityEvent OnDeathVisuals;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died!");

        // 1. Trigger C# Event for systems/managers
        OnPlayerDeath?.Invoke();

        // 2. Trigger Unity Event for audio/visuals
        OnDeathVisuals?.Invoke();

        // 3. Spawn explosion/death particles
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        }

        // 4. Clean Physics Handling: Disable forces safely
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero; // Unity 6 syntax (replaces velocity)
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // Stop responding to physics
        }

        // 5. Disable movement scripts and colliders instead of Destroy()
        // This prevents the CameraFollow script from breaking!
        foreach (Behaviour component in componentsToDisable)
        {
            if (component != null)
            {
                component.enabled = false;
            }
        }
    }
}
