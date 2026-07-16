using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 40;

    public int damage = 20;

    void Update()
    {
        transform.position +=
            transform.forward *
            speed *
            Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Health hp =
            collision.gameObject.GetComponent<Health>();

        if (hp != null)
            hp.TakeDamage(damage);

        Destroy(gameObject);
    }
}