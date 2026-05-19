using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class FireShell : MonoBehaviour
{
    [FormerlySerializedAs("speed")] public float projectileSpeed = 15;
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 5.0f;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public Transform turretTransform;
    public GameObject enemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        float? angle = RotateTurret();
        if (angle!=null)
        {
            CreateBullet();
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
    }
    
    void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        shell.GetComponent<Rigidbody>().linearVelocity = projectileSpeed * turretTransform.forward;
    }

    float? RotateTurret()
    {
        float? angle = CalculateAngle(false);
        if (angle != null)
        {
            turretTransform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }

        return angle;
    }
    
    float? CalculateAngle(bool low)
    {
        //Projectile motion equation: https://en.wikipedia.org/wiki/Projectile_motion#Angle_%CE%B8_required_to_hit_coordinate_(x,_y)
        Vector3 directionOfTarget = enemy.transform.position - transform.position;
        float y = directionOfTarget.y;
        directionOfTarget.y = 0f;
        float x = directionOfTarget.magnitude;
        float gravity = 9.81f;
        float speedSqr = projectileSpeed * projectileSpeed;
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
}
