using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbControll : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded;
    public float climbHeight = 1.5f;       // 상자 위로 올라갈 높이
    public float climbSpeed = 5f;          // 상자 위로 올라가는 속도
    public LayerMask climbableLayer;       // 올라갈 수 있는 오브젝트의 레이어

    private Rigidbody rb;
    private bool isClimbing = false;       // 현재 상자에 올라가는 중인지 여부
    private Vector3 targetPosition;        // 플레이어가 올라갈 목표 위치

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isClimbing) // 올라가는 중이 아닐 때만 이동 및 점프 가능
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
            if (Input.GetKeyDown(KeyCode.E) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        // 스페이스바 눌렀을 때 상자에 붙어서 올라가는 기능
        if (!isClimbing && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 플레이어 앞에 상자가 있는지 확인 (Raycast)
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, climbableLayer))
            {
                // 상자를 감지했으면 올라갈 위치 계산
                targetPosition = new Vector3(hit.transform.position.x, hit.transform.position.y + climbHeight, hit.transform.position.z);

                // 코루틴 시작해서 상자에 올라가기
                StartCoroutine(Climb());
            }
        }
    }

    // 상자에 올라가는 코루틴
    IEnumerator Climb()
    {
        isClimbing = true;

        // 플레이어가 목표 위치로 이동할 때까지 이동 처리
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, climbSpeed * Time.deltaTime);
            yield return null;
        }

        // 정확한 위치에 도달 후 상자 위에서 정지
        transform.position = targetPosition;

        // 상자 위로 올라가는 동작 종료
        isClimbing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(gameObject);
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
