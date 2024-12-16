using UnityEngine;

public class RotateLoadScreen : MonoBehaviour
{
    public Vector3 RotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateSpeed * Time.deltaTime);
    }
}
