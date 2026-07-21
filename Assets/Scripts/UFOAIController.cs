using UnityEngine;
using UnityEngine.UI;

public class UFOAIController : MonoBehaviour
{
    [Header("AI Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("World Space UI Elements")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Transform canvasTransform;

    private Transform playerTarget;
    private Camera mainCamera;
    private Health ufoHealth;
    private Rigidbody rb;

    private void Start()
    {
        mainCamera = Camera.main;
        ufoHealth = GetComponent<Health>();
        rb = GetComponent<Rigidbody>();

        // Enforce script translation rules over environmental physics forces
        if (rb != null) rb.isKinematic = true;

        if (ufoHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = ufoHealth.ObjectHealth;
            healthSlider.value = ufoHealth.ObjectHealth;
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) playerTarget = player.transform;
    }

    private void Update()
    {
        BillboardUI();
        SyncSlider();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (playerTarget == null || rb == null) return;

        Vector3 direction = (playerTarget.position - transform.position).normalized;
        Vector3 nextStep = transform.position + direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextStep);

        if (direction != Vector3.zero)
        {
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.fixedDeltaTime * 6f));
        }
    }

    private void BillboardUI()
    {
        if (canvasTransform != null && mainCamera != null)
        {
            canvasTransform.LookAt(canvasTransform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }

    private void SyncSlider()
    {
        if (ufoHealth != null && healthSlider != null)
        {
            healthSlider.value = ufoHealth.ObjectHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Instantly kill player ship on contact crash
        if (other.CompareTag("Player") && other.TryGetComponent<Health>(out Health playerHealth))
        {
            playerHealth.TakeDamage(playerHealth.ObjectHealth);
        }
    }
}
