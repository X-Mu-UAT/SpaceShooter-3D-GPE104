using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    [Header("Ceiling Boundaries")]
    [SerializeField] private float yMovementLimit = 15f;

    [Header("Designer Entity Limits")]
    [Tooltip("Max number of UFOs to spawn across your UFO spawn points.")]
    [SerializeField] private int ufoToSpawnCount = 3;
    [Tooltip("Max number of Health Packs to spawn.")]
    [SerializeField] private int healthPacksToSpawnCount = 2;
    [Tooltip("Max number of Astronauts to spawn.")]
    [SerializeField] private int astronautsToSpawnCount = 4;

    [Header("Prefab References")]
    [SerializeField] private GameObject ufoPrefab;
    [SerializeField] private GameObject healthPackPrefab;
    [SerializeField] private GameObject astronautPrefab;

    private List<SpawnPoint> allLevelSpawnPoints = new List<SpawnPoint>();
    public float YMovementLimit => yMovementLimit;

    private void Awake()
    {
        // Safe, non-deprecated search for scene tracking elements
        SpawnPoint[] foundPoints = Object.FindObjectsByType<SpawnPoint>(FindObjectsInactive.Include);
        allLevelSpawnPoints.AddRange(foundPoints);

        // Process dynamic random allocations across categories
        SpawnGroup(SpawnPoint.EntityType.UFO, ufoPrefab, ufoToSpawnCount);
        SpawnGroup(SpawnPoint.EntityType.HealthPack, healthPackPrefab, healthPacksToSpawnCount);
        SpawnGroup(SpawnPoint.EntityType.Astronaut, astronautPrefab, astronautsToSpawnCount);
    }

    private void SpawnGroup(SpawnPoint.EntityType targetType, GameObject prefab, int targetCount)
    {
        if (prefab == null) return;

        // Gather all matching designer points for this entity type
        List<SpawnPoint> validPoints = allLevelSpawnPoints.FindAll(p => p.AllowedType == targetType);
        int spawnCount = Mathf.Min(targetCount, validPoints.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, validPoints.Count);
            SpawnPoint point = validPoints[randomIndex];

            Instantiate(prefab, point.transform.position, point.transform.rotation);
            validPoints.RemoveAt(randomIndex); // Protect against duplicate overlapping spawns
        }
    }
}
