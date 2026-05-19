using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

    public GameObject explosion;
    public float mass = 1.0f;
    public float force = 1.0f;
    private float _forwardSpeedMetersPerSecond = 0;
    private float _forwardAcceleration;
    private float _drag = 1.0f;
    private float _gravity = -9.81f;
    private float _gravityAcceleration;
    private float _downardSpeedMetersPerSecond = 0;
    

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _forwardAcceleration = force / mass;
        _forwardSpeedMetersPerSecond += _forwardAcceleration * 1;
        _gravityAcceleration = _gravity / mass;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _forwardSpeedMetersPerSecond *= 1 - Time.deltaTime * _drag;
        _downardSpeedMetersPerSecond += _gravityAcceleration*Time.deltaTime;;
        transform.Translate(_forwardSpeedMetersPerSecond * Time.deltaTime * transform.forward, Space.World);
        transform.Translate(_downardSpeedMetersPerSecond * Time.deltaTime * Vector3.up, Space.World);
    }
}
