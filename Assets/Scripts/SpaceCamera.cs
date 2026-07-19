using UnityEngine;


public class SpaceCamera : MonoBehaviour
{
    public Transform TargetShip;
    [Range(1f, 25f)] public float PositionCatchUpSpeed = 10.0f;
    [Range(1f, 25f)] public float RotationCatchUpSpeed = 10.0f;

    private float idealDistance = 7.0f;
    private float heightOffset = 2.0f;

    void Start()
    {
        if (TargetShip == null)
        {
            Controller foundShip = Object.FindAnyObjectByType<Controller>();
            if (foundShip != null) TargetShip = foundShip.transform;
        }
    }

    public void InitializeSettings(float startDistance, float startHeight)
    {
        if (startDistance > 0f) idealDistance = startDistance;
        if (startHeight > 0f) heightOffset = startHeight;
    }

    public void UpdateDistance(float newDistance)
    {
        idealDistance = newDistance;
    }

    void LateUpdate()
    {
        if (TargetShip == null) return;

        // Calculate position relative to target ship direction matrix
        Vector3 localOffset = new Vector3(0.0f, heightOffset, -idealDistance);
        Vector3 targetWorldPosition = TargetShip.TransformPoint(localOffset);

        // Smooth translation and rotation
        transform.position = Vector3.Lerp(transform.position, targetWorldPosition, PositionCatchUpSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(TargetShip.forward, TargetShip.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationCatchUpSpeed * Time.deltaTime);
    }
}