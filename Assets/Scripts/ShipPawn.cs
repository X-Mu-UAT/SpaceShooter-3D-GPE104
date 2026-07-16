using UnityEngine;


public class ShipPawn : MonoBehaviour
{
    public Rigidbody rb;

    public float moveForce = 25f;
    public float turnSpeed = 80f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.forward * moveForce);

        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-transform.forward * moveForce);

        if (Input.GetKey(KeyCode.A))
            rb.AddTorque(Vector3.up * -turnSpeed);

        if (Input.GetKey(KeyCode.D))
            rb.AddTorque(Vector3.up * turnSpeed);

        if (Input.GetKey(KeyCode.Q))
            rb.AddTorque(Vector3.forward * turnSpeed);

        if (Input.GetKey(KeyCode.E))
            rb.AddTorque(Vector3.forward * -turnSpeed);

        if (Input.GetKey(KeyCode.Z))
            rb.AddTorque(Vector3.right * turnSpeed);

        if (Input.GetKey(KeyCode.X))
            rb.AddTorque(Vector3.right * -turnSpeed);
    }
}