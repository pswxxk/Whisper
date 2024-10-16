using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbControll : MonoBehaviour
{
    [Header("Climbing Settings")]
    public float climbDistance = 2f;          // Raycast로 감지할 거리
    public float climbSpeed = 2f;             // 클라이밍 속도 (단위: 유닛/초)
    public LayerMask climbableLayer;          // 클라이밍 가능한 오브젝트 레이어

    [Header("Climbing Offsets")]
    public float climbTopOffset = 1f;         // 클라이밍 완료 후 플레이어를 오브젝트 위로 이동하기 위한 y축 오프셋
    public float climbCenterOffset = 1f;      // 클라이밍 완료 후 플레이어를 오브젝트 중앙으로 이동하기 위한 방향 오프셋

    private Rigidbody rb;
    private bool isClimbing = false;          // 현재 클라이밍 중인지 여부
    private Vector3 climbTargetPosition;       // 클라이밍 목표 위치
    private Vector3 climbCenterPosition;       // 클라이밍 완료 후 중앙 위치
    private Vector3 climbDirection;            // Climb direction based on hit.normal

    // Raycast 결과를 시각화하기 위한 변수
    private bool lastRaycastHit = false;
    private Vector3 lastHitPoint;

    // LineRenderer 참조
    private LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // LineRenderer 초기화
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 기본 쉐이더 설정
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = false;
    }

    void Update()
    {
        // 클라이밍 중일 때는 클라이밍 입력을 무시
        if (isClimbing)
            return;

        // 클라이밍 입력 (예: F 키)
        if (Input.GetKeyDown(KeyCode.F))
        {
            AttemptClimb();
        }

        // 실시간 Raycast 시각화
        VisualizeRaycast();
    }

    void AttemptClimb()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 1f; // 플레이어의 약간 위 위치
        Vector3 rayDirection = transform.forward;                // 플레이어의 정면 방향
        float rayDistance = climbDistance;

        // 디버깅을 위해 Raycast 시각화 (1초 동안 초록색 선 표시)
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green, 1f);
        Debug.Log($"Attempting Climb: Origin={rayOrigin}, Direction={rayDirection}, Distance={rayDistance}");

        // Raycast를 통해 클라이밍 가능한 오브젝트 감지
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, climbableLayer))
        {
            Debug.Log("Climbable object detected: " + hit.collider.gameObject.name);

            // Climb direction is the normal of the hit surface
            climbDirection = hit.normal;

            // 클라이밍 목표 위치 계산 (오브젝트의 상단 중앙 + climbTopOffset)
            Bounds bounds = hit.collider.bounds;
            float targetY = bounds.max.y + climbTopOffset;
            climbTargetPosition = new Vector3(bounds.center.x, targetY, bounds.center.z);

            // 클라이밍 완료 후 중앙 위치 설정 (오브젝트 중앙 방향으로 climbCenterOffset 만큼 이동)
            climbCenterPosition = climbTargetPosition + climbDirection * climbCenterOffset;

            // Raycast 결과 저장
            lastRaycastHit = true;
            lastHitPoint = hit.point;

            // 클라이밍 시작
            StartCoroutine(ClimbToPosition(climbTargetPosition, climbCenterPosition));
        }
        else
        {
            Debug.Log("No climbable object detected.");
            lastRaycastHit = false;
        }
    }

    IEnumerator ClimbToPosition(Vector3 targetPos, Vector3 centerPos)
    {
        isClimbing = true;

        Debug.Log("Climbing started.");

        // Rigidbody의 중력 비활성화
        rb.useGravity = false;

        // 클라이밍 동안 속도 초기화
        rb.velocity = Vector3.zero;

        // 클라이밍 진행 - 첫 번째 단계: Climb to targetPos (오브젝트 상단으로 이동)
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

        // 정확한 목표 위치로 설정
        rb.MovePosition(targetPos);

        // 클라이밍 진행 - 두 번째 단계: Move to centerPos (오브젝트 중앙 위로 이동)
        while (Vector3.Distance(transform.position, centerPos) > 0.1f)
        {
            Vector3 direction = (centerPos - transform.position).normalized;
            Vector3 movement = direction * climbSpeed * Time.deltaTime;

            // 오버슈트 방지
            if (Vector3.Distance(transform.position, centerPos) < movement.magnitude)
            {
                movement = centerPos - transform.position;
            }

            rb.MovePosition(transform.position + movement);

            yield return null;
        }

        // 정확한 중앙 위치로 설정
        rb.MovePosition(centerPos);

        // 클라이밍 완료
        rb.useGravity = true;
        isClimbing = false;

        Debug.Log("Climbing finished.");
    }

    void VisualizeRaycast()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 1f; // 플레이어의 약간 위 위치
        Vector3 rayDirection = transform.forward;                // 플레이어의 정면 방향
        float rayDistance = climbDistance;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, climbableLayer))
        {
            // Raycast가 충돌했을 때
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            lineRenderer.SetPosition(0, rayOrigin);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // Raycast가 충돌하지 않았을 때
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
            lineRenderer.SetPosition(0, rayOrigin);
            lineRenderer.SetPosition(1, rayOrigin + rayDirection * climbDistance);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 에디터에서 선택 시 Raycast 시각화
        Gizmos.color = lastRaycastHit ? Color.red : Color.green;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.05f;
        Vector3 rayDirection = transform.forward;
        Gizmos.DrawRay(rayOrigin, rayDirection * climbDistance);

        if (lastRaycastHit)
        {
            // Raycast가 충돌한 지점에 작은 구 표시
            Gizmos.DrawSphere(lastHitPoint, 0.1f);
        }
    }
}
