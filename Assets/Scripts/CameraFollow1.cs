using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    [Header("Tracking Targets")]
    [SerializeField] private Transform playerTarget;

    [Header("Position Settings")]
    [SerializeField] private Vector3 followOffset = new Vector3(0f, 4f, -8f);
    [SerializeField] private float smoothTime = 0.2f;

    [Header("Look At Settings")]
    [SerializeField] private float lookAheadDistance = 10f;
    [SerializeField] private float maxRotationSpeed = 120f; // Expressed in degrees per second

    private Vector3 positionVelocity;

    private void LateUpdate()
    {
        // Safe check: Prevent console errors if the player dies or is unassigned
        if (playerTarget == null) return;

        // Calculate the target position relative to player's rotation
        Vector3 desiredPos = playerTarget.position + playerTarget.TransformDirection(followOffset);

        // SmoothDamp handles frame-rate fluctuations perfectly
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref positionVelocity, smoothTime);

        // Calculate looking point
        Vector3 targetLookPoint = playerTarget.position + (playerTarget.forward * lookAheadDistance);

        // Prevent errors if the camera is exactly on top of the target look point
        Vector3 lookDirection = targetLookPoint - transform.position;
        if (lookDirection.sqrMagnitude > 0.001f)
        {
            // Calculate the exact rotation needed to gaze at the look point
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // MODERN FIX: RotateTowards safely uses delta time without breaking the compiler
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                maxRotationSpeed * Time.deltaTime
            );
        }
    }
}
