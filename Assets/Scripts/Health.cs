using UnityEngine;

public class Health : MonoBehaviour
{
    public int ObjectHealth = 100;

    [Header("Score Configuration")]
    [Tooltip("Check this if you want the player to get points when this object dies.")]
    [SerializeField] private bool awardsScoreOnDeath = true;
    [SerializeField] private int scoreValue = 250;

    private Death death;

    void Start()
    {
        death = GetComponent<Death>();
    }

    public virtual void TakeDamage(int damage)
    {
        ObjectHealth -= damage;

        if (ObjectHealth <= 0)
        {
            // Trigger the score right before the Death script executes and destroys the object
            if (awardsScoreOnDeath && ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }

            if (death != null)
            {
                death.DoDeath();
            }
        }
    }
}
