using UnityEngine;

public class PlayerCeilingClamp : MonoBehaviour
{
    private Level currentLevel;

    private void Start()
    {
        // Safe, non-obsolete lookup for the unique level instance
        currentLevel = Object.FindAnyObjectByType<Level>();
    }

    private void LateUpdate()
    {
        if (currentLevel == null) return;

        // Teleport the ship instantly downward if it breaches the designer-defined Y height
        if (transform.position.y > currentLevel.YMovementLimit)
        {
            transform.position = new Vector3(transform.position.x, currentLevel.YMovementLimit, transform.position.z);
        }
    }
}
