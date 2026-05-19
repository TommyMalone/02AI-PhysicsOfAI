using UnityEngine;
using UnityEngine.InputSystem;

public class FireShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions["Attack"].WasReleasedThisFrame())
        {
            Vector3 aimAt = CalculateTrajectory();
            if (aimAt != Vector3.zero)
            {
                transform.forward = aimAt;
            }
            CreateBullet();
        }
    }
    
    void CreateBullet()
    {
        Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }

    Vector3 CalculateTrajectory()
    {
        Vector3 relativePositionOfTarget = enemy.transform.position - transform.position;
        Vector3 velocityOfTarget = enemy.transform.forward * enemy.GetComponent<Drive>().speed;
        float projectileSpeed = bullet.GetComponent<MoveShell>().speedMetersPerSecond;

        //Solving for terms in the quadratic equation https://en.wikipedia.org/wiki/Quadratic_equation
        float a = Vector3.Dot(velocityOfTarget, velocityOfTarget) - projectileSpeed * projectileSpeed;
        float b = Vector3.Dot(relativePositionOfTarget, velocityOfTarget);
        float c = Vector3.Dot(relativePositionOfTarget, relativePositionOfTarget);
        float d = b * b - a * c;
        
        if (d < 0.1f)
        {
            return Vector3.zero;
        }

        float sqrtD = Mathf.Sqrt(d);
        float t1 = (-b - sqrtD) / c;
        float t2 = (-b + sqrtD) / c;
        
        float t = 0;
        if (t1 < 0 && t2 < 0)
        {
            t = 0;
        }
        else if (t1 < 0)
        {
            t = t2;
        }
        else if (t2<0)
        {
            t = t1;
        }
        else
        {
            t=Mathf.Max(t1, t2);
        }

        return t * relativePositionOfTarget + velocityOfTarget;

    }
}
