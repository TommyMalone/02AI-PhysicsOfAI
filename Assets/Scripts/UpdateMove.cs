using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMove : MonoBehaviour
{
    public bool doesAccountForDeltaTime = false;
    public float speed = 1;
    void Update()
    {
        Vector3 velocity = new Vector3(0, 0, speed*(doesAccountForDeltaTime ? Time.deltaTime : 1));
        transform.Translate(velocity, Space.World);
    }
}
