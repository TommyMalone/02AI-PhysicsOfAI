using UnityEngine;

namespace Tanks
{
    public class MoveShell : MonoBehaviour
    {
        public float speedMetersPerSecond = 1.0f;
        // Update is called once per frame
        private void Update()
        {
            transform.Translate(speedMetersPerSecond * Time.deltaTime * transform.forward, Space.World);
        }
    }
}