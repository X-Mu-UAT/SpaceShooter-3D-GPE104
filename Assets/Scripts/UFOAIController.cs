using UnityEngine;
using UnityEngine.UI;

public class UFOAIController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("UI Health Bar")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private int maxHealth = 50;

    private Transform playerTarget;
    private Camera mainCamera;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null) healthSlider.maxValue = maxHealth;
        UpdateHealthBar();

        mainCamera = Camera.main;

        // Find player without relying on deprecated types
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) playerTarget = player.transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
        BillboardUI();
    }

    private void MoveTowardsPlayer()
    {
        if (playerTarget == null) return;

        // Strictly position-based translation movement bypassing physics engines
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Face the player target smoothly
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
    }

    private void BillboardUI()
    {
        // Forces World Space UI elements to directly square up with the Main Camera plane
        if (canvasTransform != null && mainCamera != null)
        {
            canvasTransform.LookAt(canvasTransform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        AudioManager.Instance.PlaySFX(SFXType.TakeDamage);

        if (currentHealth <= 0)
        {
            AudioManager.Instance.PlaySFX(SFXType.Die);
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null) healthSlider.value = currentHealth;
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
