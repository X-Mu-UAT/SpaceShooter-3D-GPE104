using UnityEngine;

public class Controller: Pawn
{
    // Camera    
    public SpaceCamera GameCamera;
    public float IdealCameraDistance = 7.0f;
    public float CameraHeightOffset = 2.0f;
    public float MinimumCameraDistance = 3.0f;
    public float MaximumCameraDistance = 10.0f;
    public float CameraOffset = 0.0f;

    // Input
    public KeyCode GoForwardKey = KeyCode.W;
    public KeyCode GoBackwardKey = KeyCode.S;
    public KeyCode YawLeftKey = KeyCode.A;
    public KeyCode YawRightKey = KeyCode.D;
    public KeyCode RollLeftKey = KeyCode.Q;
    public KeyCode RollRightKey = KeyCode.E;
    public KeyCode PitchLeftKey = KeyCode.Z;
    public KeyCode PitchRightKey = KeyCode.X;
    public KeyCode CameraOutKey = KeyCode.O;
    public KeyCode CameraInKey = KeyCode.L;
    public KeyCode ShootKey = KeyCode.F;

    // Handling Input
    private float Driving = 0f;
    private float Pitching = 0f;
    private float Yawing = 0f;
    private float Rolling = 0f;
    private float CameraMoving = 0f;

    // Track the runtime zoom state
    private float currentCameraDistance;

    protected override void Start()
    {
        base.Start();

        // Initialize tracking distance from our configuration setting
        currentCameraDistance = IdealCameraDistance;

        // If the GameCamera slot is empty, go find the SpaceCamera component in thescene
        if (GameCamera == null)
        {
            GameCamera = Object.FindAnyObjectByType<SpaceCamera>();
        }

        // Pass configuration down to the camera at startup
        if (GameCamera != null)
        {
            GameCamera.InitializeSettings(currentCameraDistance, CameraHeightOffset);
        }
    }


    protected override void Update()
    {
        base.Update();

        // Reset tracking states every frame so letting go of buttons stops the movement
        Driving = 0f; Yawing = 0f; Rolling = 0f; Pitching = 0f; CameraMoving = 0f;

        // Movement Key Captures
        if (Input.GetKey(GoForwardKey)) { Driving += 1f; }
        if (Input.GetKey(GoBackwardKey)) { Driving -= 1f; }
        if (Input.GetKey(YawLeftKey)) { Yawing -= 1f; } // Standard A key turns left
        if (Input.GetKey(YawRightKey)) { Yawing += 1f; }
        if (Input.GetKey(RollLeftKey)) { Rolling += 1f; }
        if (Input.GetKey(RollRightKey)) { Rolling -= 1f; }
        if (Input.GetKey(PitchLeftKey)) { Pitching += 1f; }
        if (Input.GetKey(PitchRightKey)) { Pitching -= 1f; }
        if (Input.GetKey(CameraOutKey)) { CameraMoving += 1f; }
        if (Input.GetKey(CameraInKey)) { CameraMoving -= 1f; }

        if (GameCamera != null && CameraMoving != 0f)
        {
            currentCameraDistance += CameraMoving * 5.0f * Time.deltaTime;

            currentCameraDistance = Mathf.Clamp(currentCameraDistance, MinimumCameraDistance, MaximumCameraDistance);
            GameCamera.UpdateDistance(currentCameraDistance);
        }


        if (Input.GetKeyDown(ShootKey))
        {
            base.Shoot();
        }
    }


    void FixedUpdate()
    {
        MoveShip(Driving, Pitching, Yawing, Rolling);
    }
}