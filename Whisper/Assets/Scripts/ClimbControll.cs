using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbControll : MonoBehaviour
{
    [Header("Climbing Settings")]
    public float climbDistance = 2f;          // Raycast로 감지할 거리
    public float climbSpeed = 2f;             // 클라이밍 속도
    public LayerMask climbableLayer;          // 클라이밍 가능한 오브젝트 레이어

    private Rigidbody rb;
    private bool isClimbing = false;          // 현재 클라이밍 중인지 여부
    private Vector3 climbTargetPosition;       // 클라이밍 목표 위치
    private Coroutine climbingCoroutine;       // 클라이밍 코루틴 참조

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 클라이밍 중일 때는 클라이밍 입력을 무시
        if (isClimbing)
            return;

        // 스페이스바를 눌렀을 때 클라이밍 시도
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttemptClimb();
        }
    }

    void AttemptClimb()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 1f; // 플레이어의 약간 위 위치
        Vector3 rayDirection = transform.right;                // 플레이어의 앞 방향
        float rayDistance = climbDistance;

        // 디버깅을 위해 Raycast 시각화
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green, 1f);

        // Raycast를 통해 클라이밍 가능한 오브젝트 감지
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, climbableLayer))
        {
            Debug.Log("Climbable object detected: " + hit.collider.gameObject.name);
            // 클라이밍 목표 위치 계산 (오브젝트의 상단 + 클라이밍 높이)
            Bounds bounds = hit.collider.bounds;
            climbTargetPosition = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);

            // 클라이밍 시작
            climbingCoroutine = StartCoroutine(ClimbToPosition(climbTargetPosition));
        }
        else
        {
            Debug.Log("No climbable object detected.");
        }
    }

    IEnumerator ClimbToPosition(Vector3 targetPos)
    {
        isClimbing = true;

        Debug.Log("Climbing started.");

        // Rigidbody의 중력 비활성화
        rb.useGravity = false;

        // 클라이밍 동안 속도 초기화
        rb.velocity = Vector3.zero;

        

        // 클라이밍 진행
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            Vector3 direction = (targetPos - transform.position).normalized;
            Vector3 movement = direction * climbSpeed * Time.deltaTime;

            // 오버슈트 방지
            if (Vector3.Distance(transform.position, targetPos) < movement.magnitude)
            {
                movement = targetPos - transform.position;
            }

            rb.MovePosition(transform.position + movement);

            yield return null;
        }

        // 정확한 위치로 설정
        rb.MovePosition(targetPos);

        // 클라이밍 완료
        rb.useGravity = true;
        isClimbing = false;

        Debug.Log("Climbing finished.");
    }

    private void OnDrawGizmosSelected()
    {
        // 에디터에서 선택 시 Raycast 시각화
        Gizmos.color = Color.green;
        Vector3 rayOrigin = transform.position + Vector3.up * 1f;
        Vector3 rayDirection = transform.forward;
        Gizmos.DrawRay(rayOrigin, rayDirection * climbDistance);
    }
}
