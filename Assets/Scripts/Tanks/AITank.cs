using UnityEngine;

public class AITank : Tank
{
    public float stopDistance = 10;
    public bool shootLowAngle = true;
    public float fireAngleTolerance = 2f;
    public GameObject enemy;

    private void FixedUpdate()
    {
        if (enemy != null)
        {
            Vector3 directionToEnemyXZ = enemy.transform.position - transform.position;
            directionToEnemyXZ.y = 0;
            bool shouldMoveForward = Vector3.Dot(transform.forward, directionToEnemyXZ) >= 0f;
            if (directionToEnemyXZ.sqrMagnitude >= stopDistance*stopDistance)
            {
                MoveTank(shouldMoveForward, Time.fixedDeltaTime);
            }
            
            Quaternion targetBodyRotation = Quaternion.LookRotation(directionToEnemyXZ.normalized * (shouldMoveForward ? 1 : -1),  Vector3.up);
            RotateTankToward(targetBodyRotation, Time.fixedDeltaTime);

            float? targetPitch = CalculateTargetTurretPitchAngle(shootLowAngle);
            float targetYaw = CalculateTargetTurretYawAngle();

            if (targetPitch.HasValue)
            {
                RotateTurretToward(targetPitch.Value, targetYaw, Time.fixedDeltaTime);
            }

            bool hasShot = targetPitch.HasValue && IsTurretAimed(targetPitch, targetYaw);
            if (CanFire() && hasShot)
            {
                FireShell();
            }
        }
    }

    private float? CalculateTargetTurretPitchAngle(bool low)
    {
        //Projectile motion equation: https://en.wikipedia.org/wiki/Projectile_motion#Angle_%CE%B8_required_to_hit_coordinate_(x,_y)
        Vector3 directionOfTarget = enemy.transform.position - shellSpawn.position;
        float y = directionOfTarget.y;
        directionOfTarget.y = 0f;
        float x = directionOfTarget.magnitude;
        float gravity = 9.81f;
        float speedSqr = initialProjectileSpeed * initialProjectileSpeed;
        float underTheSqrRoot = speedSqr*speedSqr - gravity *(gravity*x*x+2*y*speedSqr);
        
        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt((underTheSqrRoot));
            float highAngle = speedSqr + root;
            float lowAngle = speedSqr - root;
            if (low)
            {
                return Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg;
            }
            else
            {
                return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg;
            }
        }
        else
        {
            return null;
        }
    }

    private float CalculateTargetTurretYawAngle()
    {
        Vector3 localTarget = transform.InverseTransformPoint(enemy.transform.position);
        localTarget.y = 0f;
        return Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
    }
        
    private bool IsTurretAimed(float? targetPitch, float targetYaw)
    {
        if (targetPitch.HasValue)
        {
            Quaternion desiredLocalRotation = Quaternion.AngleAxis(targetYaw, Vector3.up) * Quaternion.AngleAxis(targetPitch.Value, Vector3.left);

            Vector3 desiredWorldForward = transform.TransformDirection(desiredLocalRotation * Vector3.forward);

            float angleError = Vector3.Angle(shellSpawn.forward, desiredWorldForward);

            return angleError <= fireAngleTolerance;
        }

        return false;
    }
}