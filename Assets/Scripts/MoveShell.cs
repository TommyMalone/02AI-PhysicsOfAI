using UnityEngine;

public class MoveShell : MonoBehaviour
{
    public float speedMetersPerSecond = 1.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(speedMetersPerSecond * Time.deltaTime * transform.forward);
    }
}
