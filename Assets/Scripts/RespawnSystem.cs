using UnityEngine;
using System.Collections;

public class SpaceshipRespawn : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float respawnDelay = 2.0f;

    [Header("Visual Effects")]
    [SerializeField] private GameObject explosionEffect;

    private Rigidbody rb;
    private MeshRenderer[] meshRenderers;
    private Collider[] colliders;
    private Pawn movementScript;
    private Health healthScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementScript = GetComponent<Pawn>();
        healthScript = GetComponent<Health>(); // Grab your existing health component

        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        colliders = GetComponentsInChildren<Collider>();

        if (spawnPoint == null)
        {
            GameObject defaultSpawn = new GameObject("Default_SpawnPoint");
            defaultSpawn.transform.position = transform.position;
            defaultSpawn.transform.rotation = transform.rotation;
            spawnPoint = defaultSpawn.transform;
        }
    }

    public void TriggerRespawn()
    {
        StartCoroutine(RespawnSequence());
    }

    private IEnumerator RespawnSequence()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        ToggleShipState(false);
        yield return new WaitForSeconds(respawnDelay);

        // Reset physical physics velocities
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Teleport back home
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        // Reset your Health component back to 100
        if (healthScript != null)
        {
            healthScript.ObjectHealth = 500;
        }

        ToggleShipState(true);
    }

    private void ToggleShipState(bool isEnabled)
    {
        if (movementScript != null) movementScript.enabled = isEnabled;
        foreach (var mesh in meshRenderers) mesh.enabled = isEnabled;
        foreach (var col in colliders) col.enabled = isEnabled;
    }
}
