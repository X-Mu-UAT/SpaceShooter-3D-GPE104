using UnityEngine;
using TMPro; // CRITICAL: Gives access to TextMeshPro text fields

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Pull the live numeric data straight from our active GameManager instance
        if (GameManager.Instance != null && textMesh != null)
        {
            textMesh.text = $"SCORE: {GameManager.Instance.GetCurrentScore()}";
        }
    }
}
