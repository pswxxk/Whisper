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
            Jump();  // 점프 실행
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 방향 설정
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // 이동 처리
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
        animator.SetBool("isJumping", true);  // 점프 애니메이션 활성화
    }
    public void Push()
    {
        isPushing = true;
        animator.SetBool("isPushing", true);
        animator.SetBool("isWalking", false);
        Debug.Log("밀어");
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

        // 점프 중이 아니고 땅에 착지했을 때만 점프 애니메이션 종료
        if (isGrounded && isJumping)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);  // 점프 애니메이션 비활성화
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
    