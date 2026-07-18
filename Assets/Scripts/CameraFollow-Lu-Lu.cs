using UnityEngine;
using UnityEngine.InputSystem; // REQUIRED for the new input package

public class CameraFollow : MonoBehaviour
{
    [Header("Tracking Targets")]
    [SerializeField] private Transform player;

    [Header("Position Settings")]
    [SerializeField] private float smoothTime = 0.2f; // Time in seconds to reach target (Lower = Snappier)
    [SerializeField] private float zoomSpeed = 5f;

    [Header("Distance Limits")]
    [SerializeField] private float offsetMagnitude = 8f;
    [SerializeField] private float minDistance = 4f;
    [SerializeField] private float maxDistance = 15f;

    [Header("Look At Settings")]
    [SerializeField] private float lookAhead = 10f;
    [SerializeField] private float rotationSmoothTime = 0.1f;

    // Internal reference vectors for smooth damp calculations
    private Vector3 positionVelocity;
    private float rotationVelocity;

    void LateUpdate()
    {
        if (player == null) return;

        // 1. MODERN INPUT HANDLING
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            // O increases camera distance
            if (keyboard.oKey.isPressed)
            {
                offsetMagnitude += Time.deltaTime * zoomSpeed;
            }

            // L decreases camera distance
            if (keyboard.lKey.isPressed)
            {
                offsetMagnitude -= Time.deltaTime * zoomSpeed;
            }
        }

        // Keep camera distance within limits
        offsetMagnitude = Mathf.Clamp(offsetMagnitude, minDistance, maxDistance);

        // Keep the same direction but change distance dynamically
        Vector3 dynamicOffset = new Vector3(0, 4, -offsetMagnitude);

        // Calculate target positions
        Vector3 desiredPosition = player.position + player.TransformDirection(dynamicOffset);

        // 2. FIXED STUTTERING: SmoothDamp handles frame-rate changes perfectly
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref positionVelocity, smoothTime);

        // 3. FIXED ROTATION JITTER: Smooth look-at calculation
        Vector3 lookPoint = player.position + player.forward * lookAhead;
        Vector3 targetDirection = lookPoint - transform.position;

        if (targetDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // Seamlessly match player rotation smoothly over time
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime / Time.deltaTime);
        }
    }
}
