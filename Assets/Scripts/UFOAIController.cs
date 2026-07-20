using UnityEngine;
using UnityEngine.UI;

public class UFOAIController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("UI Health Bar")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Transform canvasTransform;

    private Transform playerTarget;
    private Camera mainCamera;
    private Health enemyHealth; // Reference to your existing health script

    private void Start()
    {
        mainCamera = Camera.main;

        // Grab the existing Health component attached to this specific UFO
        enemyHealth = GetComponent<Health>();

        // Initialize the slider based on your existing health system variables
        if (enemyHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = enemyHealth.ObjectHealth;
            healthSlider.value = enemyHealth.ObjectHealth;
        }

        // Find player without relying on deprecated types
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) playerTarget = player.transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
        BillboardUI();
        UpdateHealthBar();
    }

    private void MoveTowardsPlayer()
    {
        if (playerTarget == null) return;

        // Strictly position-based translation movement bypassing physics engines
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Face the player target smoothly
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
        }
    }

    private void BillboardUI()
    {
        // Forces World Space UI elements to directly square up with the Main Camera plane
        if (canvasTransform != null && mainCamera != null)
        {
            canvasTransform.LookAt(canvasTransform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }

    private void UpdateHealthBar()
    {
        // Continuously read the exact ObjectHealth from your script to update the UI
        if (enemyHealth != null && healthSlider != null)
        {
            healthSlider.value = enemyHealth.ObjectHealth;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health playerHealth))
        {
            // Instantly kill the player by forcing maximum structural damage
            playerHealth.TakeDamage(playerHealth.ObjectHealth);
        }
    }
}
