using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Designer Configurations")]
    [Tooltip("Drop the matching item Prefab variant here (Health pack, Astronaut, or UFO archetype).")]
    [SerializeField] private GameObject entityPrefab;

    public GameObject EntityPrefab => entityPrefab;

    private void OnDrawGizmos()
    {
        // Visual indicator in Scene View for designers
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.75f);
        Gizmos.DrawRay(transform.position, transform.forward * 1.5f);
    }
}
