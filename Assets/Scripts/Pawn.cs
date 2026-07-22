using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pawn : MonoBehaviour
{
    public float thrust = 500.0f;
    public float PitchSpeed = 15.0f;
    public float YawSpeed = 15.0f;
    public float RollSpeed = 20.0f;

    public GameObject ProjectilePrefab;
    public Transform MuzzlePoint;

    protected Rigidbody Rigid;
    protected GameManager gameManager;

    // Cached fields to safely pass input values from Update to FixedUpdate
    protected float cachedDriving;
    protected float cachedPitch;
    protected float cachedYaw;
    protected float cachedRoll;

    protected virtual void Start()
    {
        Rigid = GetComponent<Rigidbody>();
        gameManager = Object.FindAnyObjectByType<GameManager>();

        Rigid.useGravity = false;

        // Erases visual stuttering by predicting object positions between frames
        Rigid.interpolation = RigidbodyInterpolation.Interpolate;
    }

    protected virtual void Update()
    {
        // Read input and assign to cached variables in child classes (e.g., PlayerController)
    }

    protected virtual void FixedUpdate()
    {
        // Executes physics calculations strictly on the physics clock
        MoveShip(cachedDriving, cachedPitch, cachedYaw, cachedRoll);
    }

    protected void MoveShip(float drivingInput, float pitchInput, float yawInput, float rollInput)
    {
        // --- FORWARD MOVEMENT & COUNTER-BRAKING ---
        if (Mathf.Abs(drivingInput) > 0.01f)
        {
            // Apply forward or backward engine thrust
            Rigid.AddRelativeForce(Vector3.forward * drivingInput * thrust * Time.fixedDeltaTime);
        }
        else
        {
            // Active Braking: Instantly bleed out forward/backward velocity when keys are released
            Vector3 localVelocity = transform.InverseTransformDirection(Rigid.linearVelocity);
            localVelocity.z = Mathf.MoveTowards(localVelocity.z, 0f, 5f * Time.fixedDeltaTime); // Adjust '5f' for faster stopping
            Rigid.linearVelocity = transform.TransformDirection(localVelocity);
        }

        // --- ROTATIONAL FORCES ---
        Rigid.AddRelativeTorque(Vector3.right * pitchInput * PitchSpeed * Time.fixedDeltaTime);
        Rigid.AddRelativeTorque(Vector3.up * yawInput * YawSpeed * Time.fixedDeltaTime);
        Rigid.AddRelativeTorque(Vector3.back * rollInput * RollSpeed * Time.fixedDeltaTime);

        // --- ACTIVE ROTATION COUNTER-BRAKING ---
        // If player is not giving rotational inputs, instantly halt the matching spin axis
        if (Mathf.Abs(pitchInput) < 0.01f && Mathf.Abs(yawInput) < 0.01f && Mathf.Abs(rollInput) < 0.01f)
        {
            // Dampens out all spinning momentum smoothly
            Rigid.angularVelocity = Vector3.MoveTowards(Rigid.angularVelocity, Vector3.zero, 10f * Time.fixedDeltaTime);
        }
    }


    public virtual void Shoot()
    {
        if (ProjectilePrefab != null)
        {
            Transform spawnPoint = MuzzlePoint != null ? MuzzlePoint : transform;
            Instantiate(ProjectilePrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("[Pawn] Cannot shoot because ProjectilePrefab is not assigned in the inspector.");
        }
    }
}
