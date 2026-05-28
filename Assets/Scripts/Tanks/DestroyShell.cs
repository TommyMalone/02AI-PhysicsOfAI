using UnityEngine;

namespace Tanks
{
    public class DestroyShell : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            Destroy(this.gameObject, 3);  
        }

    }
}