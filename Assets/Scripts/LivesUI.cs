using UnityEngine;
using TMPro; // CRITICAL: Gives access to TextMeshPro text elements

public class LivesUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        // Cache the TextMeshPro component attached to this GameObject
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Continuously read the remaining lives from the GameManager
        if (GameManager.Instance != null && textMesh != null)
        {
            textMesh.text = $"LIVES: {GameManager.Instance.GetRemainingLives()}";
        }
    }
}
