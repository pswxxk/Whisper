using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;

    private bool isGrounded;
    private bool isJumping;
    private bool isPushing;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Rotate();
        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();  // ���� ����
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // ���� ����
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // �̵� ó��
        if (moveDirection.magnitude > 0.1f)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void Rotate()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        isGrounded = false;
        isJumping = true;
        animator.SetBool("isJumping", true);  // ���� �ִϸ��̼� Ȱ��ȭ
    }
    public void Push()
    {
        isPushing = true;
        animator.SetBool("isPushing", true);
        animator.SetBool("isWalking", false);
        Debug.Log("�о�");
    }

    public void StopPushing()
    {
        isPushing = false;
        animator.SetBool("isPushing", false);
    }

    void UpdateAnimation()
    {
        bool isWalking = moveDirection.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);

        // ���� ���� �ƴϰ� ���� �������� ���� ���� �ִϸ��̼� ����
        if (isGrounded && isJumping)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);  // ���� �ִϸ��̼� ��Ȱ��ȭ
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
    