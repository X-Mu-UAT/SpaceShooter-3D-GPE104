using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum EntityType { UFO, HealthPack, Astronaut }

    [Header("Spawn Settings")]
    [Tooltip("What type of entity is allowed to spawn at this specific landmark point?")]
    [SerializeField] private EntityType allowedType;

    public EntityType AllowedType => allowedType;

    private void OnDrawGizmos()
    {
        // Color code anchors visually for your designers in the scene window
        Gizmos.color = allowedType switch
        {
            EntityType.UFO => Color.red,
            EntityType.HealthPack => Color.green,
            EntityType.Astronaut => Color.cyan,
            _ => Color.white
        };
        Gizmos.DrawWireSphere(transform.position, 0.75f);
        Gizmos.DrawRay(transform.position, transform.forward * 1.2f);
    }
}
