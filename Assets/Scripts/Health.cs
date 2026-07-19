using UnityEngine;

public class Health : MonoBehaviour
{
    public int ObjectHealth = 100;

    private Death death;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        death = GetComponent<Death>();
    }

    public virtual void TakeDamage(int damage)
    {
        ObjectHealth -= damage;
        if (ObjectHealth <= 0)
        {
            death.DoDeath();
        }
    }
}