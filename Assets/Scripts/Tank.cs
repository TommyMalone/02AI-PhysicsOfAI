using UnityEngine;

public class Tank : MonoBehaviour
{
    public float initialProjectileSpeed = 15;
    public float movementSpeed = 1;
    public float rotationSpeed = 5;
    public float maxPitch = 80;
    public float minPitch = 0;
    public float turretMaxYawRotationSpeed = 30;
    public float turretMaxPitchRotationSpeed = 30;
    [Range(0.1f, 5)]
    public float shotsPerSecond = 1;
    public GameObject shellType;
    public Transform shellSpawn;
    public Transform turretTransform;
    protected float LastFireTime;
    private Collider _collider;
    private float _turretYaw;
    private float _turretPitch;
        
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    protected void MoveTank(bool moveForward, float deltaTime)
    {
        float moveDirection = moveForward ? 1 : -1;
        transform.Translate(transform.forward * (moveDirection * movementSpeed * deltaTime), Space.World);
    }
        
    protected void RotateTank(bool rotateClockwise, float deltaTime)
    {
        float turnDirection = rotateClockwise ? 1 : -1;
        Quaternion turnRotation = Quaternion.AngleAxis(turnDirection * rotationSpeed * deltaTime, Vector3.up);
        Quaternion newRotation = turnRotation * transform.rotation;
        transform.rotation = newRotation;
    }
    protected void RotateTankToward(Quaternion lookRotation,float deltaTime)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, deltaTime * rotationSpeed);
    }
        
    protected void RotateTurret(float deltaPitch, float deltaYaw,float deltaTime)
    {
        deltaPitch = Mathf.Clamp(deltaPitch, -turretMaxPitchRotationSpeed * deltaTime, turretMaxPitchRotationSpeed * deltaTime);
        _turretPitch += deltaPitch;
        _turretPitch = Mathf.Clamp(_turretPitch, minPitch, maxPitch);
        deltaYaw = Mathf.Clamp(deltaYaw, -turretMaxYawRotationSpeed * deltaTime, turretMaxYawRotationSpeed * deltaTime);
        _turretYaw += deltaYaw;

        Quaternion yawRotation = Quaternion.AngleAxis(_turretYaw, Vector3.up);
        Quaternion pitchRotation = Quaternion.AngleAxis(_turretPitch, Vector3.left);

        turretTransform.localRotation = yawRotation * pitchRotation;
    }
        
    protected void RotateTurretToward(float targetPitch, float targetYaw, float deltaTime)
    {
        float deltaPitch = Mathf.DeltaAngle(_turretPitch, targetPitch);
        float deltaYaw = Mathf.DeltaAngle(_turretYaw, targetYaw);

        if (deltaPitch != 0 || deltaYaw != 0)
        {
            RotateTurret(deltaPitch, deltaYaw, deltaTime);
        }
    }

    protected bool CanFire()
    {
        return LastFireTime + shotsPerSecond <= Time.time;
    }
        
    protected void FireShell()
    {
        LastFireTime = Time.time;
        GameObject newShell = Instantiate(shellType, shellSpawn.position, shellSpawn.rotation);
        Physics.IgnoreCollision(newShell.GetComponent<Collider>(), _collider);
        newShell.GetComponent<Rigidbody>().linearVelocity = initialProjectileSpeed * shellSpawn.forward;
    }
}