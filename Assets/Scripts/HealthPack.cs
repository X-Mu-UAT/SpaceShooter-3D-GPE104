using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int healAmount = 25;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatMagnitude = 0.5f;

    private Vector3 startPos;

    private void Start() => startPos = transform.position;

    private void Update()
    {
        // Floating action in space
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatMagnitude, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health playerHealth))
        {
            playerHealth.ObjectHealth = Mathf.Min(playerHealth.ObjectHealth + healAmount, 100);

            // Audio hook for pickups
            AudioManager.Instance.PlaySFX(SFXType.HealthPickup);
            Destroy(gameObject);
        }
    }
}
