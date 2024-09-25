using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  
    public float zOffset = -17f;  
    private float yDifference;    
    void Start()
    {
        yDifference = transform.position.y - player.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + yDifference, zOffset);
    }
}