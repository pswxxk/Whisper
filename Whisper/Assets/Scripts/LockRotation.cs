using UnityEngine;

public class LockRotation : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}