using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ShipPawn : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 250f; // Increased default since modern physics scaling differs
    [SerializeField] private float turnSpeed = 800f;

    private Rigidbody rb;

    // Cache input values
    private float moveInput;
    private float yawInput;
    private float rollInput;
    private float pitchInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Read keys using the modern Keyboard API
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        // W / S for Forward/Backward
        moveInput = 0f;
        if (keyboard.wKey.isPressed) moveInput += 1f;
        if (keyboard.sKey.isPressed) moveInput -= 1f;

        // A / D for Yaw (Turning Left/Right)
        yawInput = 0f;
        if (keyboard.dKey.isPressed) yawInput += 1f;
        if (keyboard.aKey.isPressed) yawInput -= 1f;

        // Q / E for Roll (Banking Left/Right)
        rollInput = 0f;
        if (keyboard.qKey.isPressed) rollInput += 1f;
        if (keyboard.eKey.isPressed) rollInput -= 1f;

        // Z / X for Pitch (Nose Up/Down)
        pitchInput = 0f;
        if (keyboard.zKey.isPressed) pitchInput += 1f;
        if (keyboard.xKey.isPressed) pitchInput -= 1f;
    }

    void FixedUpdate()
    {
        // Calculate deltaTime-independent force vectors
        float dt = Time.fixedDeltaTime;

        // Apply Linear Force
        if (moveInput != 0)
        {
            rb.AddForce(transform.forward * (moveInput * moveForce * dt), ForceMode.VelocityChange);
        }

        // Apply Rotational Torque (Yaw, Roll, Pitch)
        if (yawInput != 0) rb.AddTorque(transform.up * (yawInput * turnSpeed * dt), ForceMode.VelocityChange);
        if (rollInput != 0) rb.AddTorque(transform.forward * (-rollInput * turnSpeed * dt), ForceMode.VelocityChange);
        if (pitchInput != 0) rb.AddTorque(transform.right * (pitchInput * turnSpeed * dt), ForceMode.VelocityChange);
    }
}
