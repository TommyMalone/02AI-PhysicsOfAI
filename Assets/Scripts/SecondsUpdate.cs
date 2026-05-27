using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    public float speed = 1;
    float timeStartOffset = 0;
    bool gotStartTime = false;
    
    void Update()
    {
        if (!gotStartTime)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, (Time.realtimeSinceStartup - timeStartOffset)*speed);
    }
}
