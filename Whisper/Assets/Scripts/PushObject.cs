using UnityEngine;

public class PushObject : MonoBehaviour
{
    public Collider pushObjects;
    private PlayerController playerController;
    private Animator animator;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerController != null)
        {
            playerController.Push();  
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerController != null)  
        {
            playerController.StopPushing();  
        }
    }
}
