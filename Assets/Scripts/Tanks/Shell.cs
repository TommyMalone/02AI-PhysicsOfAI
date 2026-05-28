using UnityEngine;

namespace Tanks
{
    public class Shell : MonoBehaviour
    {
        public GameObject explosion;
        public float mass = 1.0f;
        public float force = 1.0f;
        private float _forwardSpeedMetersPerSecond = 0;
        private float _forwardAcceleration;
        private const float Drag = 1.0f;
        private const float Gravity = -9.81f;
        private float _gravityAcceleration;
        private float _downwardSpeedMetersPerSecond = 0;


        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag($"tank"))
            {
                GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(exp, 0.5f);
                Destroy(this.gameObject);
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            _forwardAcceleration = force / mass;
            _forwardSpeedMetersPerSecond += _forwardAcceleration * 1;
            _gravityAcceleration = Gravity / mass;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            _forwardSpeedMetersPerSecond *= 1 - Time.deltaTime * Drag;
            _downwardSpeedMetersPerSecond += _gravityAcceleration*Time.deltaTime;;
            transform.Translate(_forwardSpeedMetersPerSecond * Time.deltaTime * transform.forward, Space.World);
            transform.Translate(_downwardSpeedMetersPerSecond * Time.deltaTime * Vector3.up, Space.World);
        }
    }
}