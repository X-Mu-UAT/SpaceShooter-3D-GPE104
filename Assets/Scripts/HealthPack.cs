using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int healAmount = 25;
    [SerializeField] private float bounceSpeed = 2f;
    [SerializeField] private float bounceMagnitude = 0.3f;

    private Vector3 startPosition;

    private void Start() => startPosition = transform.position;

    private void Update()
    {
        // Clean non-physics float offset tracking loop 
        transform.position = startPosition + new Vector3(0f, Mathf.Sin(Time.time * bounceSpeed) * bounceMagnitude, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health playerHealth))
        {
            // Track health before the healing occurs
            int previousHealth = playerHealth.ObjectHealth;

            // Restore structural points safely caps at 500 max to match player health
            playerHealth.ObjectHealth = Mathf.Min(playerHealth.ObjectHealth + healAmount, 500);

            // Calculate exact health gained (prevents over-reporting if already near 500)
            int actualHealedAmount = playerHealth.ObjectHealth - previousHealth;

            // Print the message directly to the Unity Console log
            Debug.Log($"I healed! Grabbed health pack. Gained: +{actualHealedAmount} HP. Current Health: {playerHealth.ObjectHealth}/500");

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX(SFXType.HealthPickup);

            Destroy(gameObject);
        }
    }
}
