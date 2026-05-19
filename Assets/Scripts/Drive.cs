using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drive : MonoBehaviour
{
    public float speed = 3.0f;
    public float tankRotationSpeed = 100.0f;
    public float verticalTurretRotationSpeed = 30.0f;
    public float horizontalTurretRotationSpeed = 30.0f;
    public Transform gunTransform;
    public Transform bulletSpawn;
    public GameObject bullet;

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = InputSystem.actions["Move"].ReadValue<Vector2>().y * speed;
        float rotation = InputSystem.actions["Move"].ReadValue<Vector2>().x * tankRotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
        
        float turretVerticalRotation = Mathf.Clamp(InputSystem.actions["Look"].ReadValue<Vector2>().y,-1,1) *verticalTurretRotationSpeed *Time.deltaTime;
        // float turretHorizontalRotation = Mathf.Clamp(InputSystem.actions["Look"].ReadValue<Vector2>().x,-1,1) *horizontalTurretSpeed *Time.deltaTime;
        
        gunTransform.RotateAround(gunTransform.position, gunTransform.right,turretVerticalRotation);
        // gunTransform.RotateAround(gunTransform.position, gunTransform.up,turretHorizontalRotation);

        if (InputSystem.actions["Interact"].WasReleasedThisFrame())
        {
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
    
}
