using UnityEditor.Experimental.GraphView;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Rigid = GetComponent<Rigidbody>();
        GameManager manager = Object.FindAnyObjectByType<GameManager>();
        Rigid.useGravity = false;
    }
    protected virtual void Update()
    {

    }
    protected void MoveShip(float drivingInput, float pitchInput, float yawInput, float rollInput)
    {
        Rigid.AddRelativeForce(Vector3.forward * drivingInput * thrust * Time.deltaTime);
        Rigid.AddRelativeTorque(Vector3.right * pitchInput * PitchSpeed * Time.deltaTime);
        Rigid.AddRelativeTorque(Vector3.up * yawInput * YawSpeed * Time.deltaTime);
        Rigid.AddRelativeTorque(Vector3.back * rollInput * RollSpeed * Time.deltaTime);
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