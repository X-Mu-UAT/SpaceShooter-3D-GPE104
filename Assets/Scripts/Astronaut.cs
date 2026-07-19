using UnityEngine;

public class Astronaut : MonoBehaviour
{
    [SerializeField] private int pointValue = 100;
    [SerializeField] private float rotationSpeed = 30f;

    private void Update()
    {
        // Float spinning in space
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Point accumulation hooks
            ScoreManager.Instance.AddScore(pointValue);
            AudioManager.Instance.PlaySFX(SFXType.AstronautPickup);
            Destroy(gameObject);
        }
    }
}
