using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(0, 4, -8);

    public float smoothSpeed = 5f;

    public float lookAhead = 10f;

    public float offsetMagnitude = 8f;

    public float minDistance = 4f;

    public float maxDistance = 15f;


    void LateUpdate()
    {
        if (player == null)
            return;


        // O increases camera distance
        if (Input.GetKey(KeyCode.O))
        {
            offsetMagnitude += Time.deltaTime * 5;
        }


        // L decreases camera distance
        if (Input.GetKey(KeyCode.L))
        {
            offsetMagnitude -= Time.deltaTime * 5;
        }


        // Keep camera distance within limits
        offsetMagnitude = Mathf.Clamp(
            offsetMagnitude,
            minDistance,
            maxDistance
        );


        // Keep the same direction but change distance
        offset = new Vector3(0, 4, -offsetMagnitude);


        // Camera position
        Vector3 desiredPosition =
            player.position +
            player.TransformDirection(offset);


        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );


        // Look ahead of the ship
        Vector3 lookPoint =
            player.position +
            player.forward * lookAhead;


        transform.LookAt(lookPoint);
    }
}