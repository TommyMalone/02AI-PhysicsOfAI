using UnityEngine;

namespace Tanks
{
    public class AIShell : MonoBehaviour
    {
        public GameObject explosion;
        private Rigidbody _rigidBody;

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
            _rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.forward = _rigidBody.linearVelocity;
        }
    }
}