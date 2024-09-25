using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;     
    public float jumpForce = 10f;     
    public bool isGrounded;           

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(move * moveSpeed, rb.velocity.y, 0);
        rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
