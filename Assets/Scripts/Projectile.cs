using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float FlightSpeed = 150.0f;
    public float Lifespan = 4.0f;
    public int DamageValue = 10;

    // Configure who this laser hurts (Player layer or Obstacles layer)
    public LayerMask TargetLayers;

    private Rigidbody Rigid;

    void Start()
    {
        Rigid = GetComponent<Rigidbody>();

        // Propel the projectile forward instantly
        Rigid.linearVelocity = transform.forward * FlightSpeed;

        // Automatically clean up 
        Destroy(gameObject, Lifespan);
    }

    void OnCollisionEnter(Collision collision)
    {
        HandleImpact(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        HandleImpact(other.gameObject);
    }

    private void HandleImpact(GameObject hitObject)
    {
        // Check if the collided object resides on a valid target layer
        if (((1 << hitObject.layer) & TargetLayers) != 0)
        {
            Health targetHealth = hitObject.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(DamageValue);
            }

            // Delete the projectile asset on impact
            Destroy(gameObject);
        }
    }
}