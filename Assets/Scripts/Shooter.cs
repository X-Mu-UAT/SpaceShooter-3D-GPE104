using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    void Update()
    {
        // Get the current keyboard instance
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Modern replacement for Input.GetKeyDown(KeyCode.Space)
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Ensure references exist before spawning to avoid errors
        if (projectilePrefab == null || firePoint == null) return;

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
