using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateUpdateMove : MonoBehaviour
{
    public bool doesAccountForDeltaTime = false;
    public float speed = 1;
    void LateUpdate()
    {
        Vector3 velocity = new Vector3(0, 0, speed*(doesAccountForDeltaTime ? Time.deltaTime : 1));
        transform.Translate(velocity, Space.World);
    }
}
