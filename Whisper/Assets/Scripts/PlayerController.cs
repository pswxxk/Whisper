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
        // 수평 이동 입력
        float move = Input.GetAxis("Horizontal");

        // 플레이어의 이동
        Vector3 movement = new Vector3(move * moveSpeed, rb.velocity.y, 0);
        rb.velocity = movement;

        // 정면 반대키를 누를 때 y축 회전값을 180도로 변경
        if (move < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // 왼쪽을 바라보는 회전
        }
        else if (move > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // 오른쪽을 바라보는 회전
        }

        // 점프 처리
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
