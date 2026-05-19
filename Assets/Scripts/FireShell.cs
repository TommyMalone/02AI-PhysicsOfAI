using UnityEngine;
using UnityEngine.InputSystem;

public class FireShell : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
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
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        if (InputSystem.actions["Attack"].WasReleasedThisFrame())
        {
            CreateBullet();
        }
    }
    
    void CreateBullet()
    {
        Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }
}
