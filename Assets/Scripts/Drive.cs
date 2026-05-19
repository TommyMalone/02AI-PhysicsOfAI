using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drive : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotationSpeed = 100.0f;

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = InputSystem.actions["Move"].ReadValue<Vector2>().y * speed;
        float rotation = InputSystem.actions["Move"].ReadValue<Vector2>().x * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        //transform.Translate(0, 0, translation);
        transform.Translate(0, 0, speed *Time.deltaTime);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }
}
