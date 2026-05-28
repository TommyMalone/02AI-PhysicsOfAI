using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    public float speed = 1;
    private float _timeStartOffset = 0;
    private bool _gotStartTime = false;

    private void Update()
    {
        if (!_gotStartTime)
        {
            _timeStartOffset = Time.realtimeSinceStartup;
            _gotStartTime = true;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, (Time.realtimeSinceStartup - _timeStartOffset)*speed);
    }
}