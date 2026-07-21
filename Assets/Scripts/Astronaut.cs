using UnityEngine;

public class Astronaut : MonoBehaviour
{
    [SerializeField] private int scoreReward = 150;
    [SerializeField] private float rotationSpeed = 45f;

    private void Update()
    {
        // Rotates smoothly in deep space
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null) ScoreManager.Instance.AddScore(scoreReward);
            if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(SFXType.AstronautPickup);
            Destroy(gameObject);
        }
    }
}
