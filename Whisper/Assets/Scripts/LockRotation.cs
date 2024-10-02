using UnityEngine;

public class LockRotation : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}