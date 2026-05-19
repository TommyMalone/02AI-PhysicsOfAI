using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private float _speedMetersPerSecond = 0;
    public GameObject explosion;
    public float mass = 10.0f;
    public float force = 1000.0f;
    public float acceleration = 10.0f;
    

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

    }

    // Update is called once per frame
    void LateUpdate()
    {
        acceleration = force / mass;
        _speedMetersPerSecond += acceleration * Time.deltaTime;
        transform.Translate(_speedMetersPerSecond * Time.deltaTime * transform.forward, Space.World);
    }
}
