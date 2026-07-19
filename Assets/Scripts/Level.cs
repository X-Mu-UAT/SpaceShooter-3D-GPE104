using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    [Header("Ceiling Settings")]
    [Tooltip("The maximum Y coordinate the player can reach.")]
    [SerializeField] private float yMovementLimit = 15f;

    [Header("Spawn Settings")]
    [Tooltip("Target number of entities to randomly distribute across available level spawn points.")]
    [SerializeField] private int totalEntitiesToSpawn = 5;

    private List<SpawnPoint> levelSpawnPoints = new List<SpawnPoint>();

    public float YMovementLimit => yMovementLimit;

    private void Awake()
    {
        // Automatically find all instances without depending on deprecated sorting
        // NEW CODE (No warning):
        SpawnPoint[] foundPoints = Object.FindObjectsByType<SpawnPoint>();
        levelSpawnPoints.AddRange(foundPoints);

        PopulateLevel();
    }

    private void PopulateLevel()
    {
        if (levelSpawnPoints.Count == 0) return;

        // Shuffle the list or pick random points without repeating
        List<SpawnPoint> availablePoints = new List<SpawnPoint>(levelSpawnPoints);
        int spawnCount = Mathf.Min(totalEntitiesToSpawn, availablePoints.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, availablePoints.Count);
            SpawnPoint selectedPoint = availablePoints[randomIndex];

            if (selectedPoint.EntityPrefab != null)
            {
                Instantiate(selectedPoint.EntityPrefab, selectedPoint.transform.position, selectedPoint.transform.rotation);
            }

            availablePoints.RemoveAt(randomIndex); // Prevent double-spawning on a single point
        }
    }
}
