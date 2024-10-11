using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbControll : MonoBehaviour
{
    [Header("Climbing Settings")]
    public float climbDistance = 2f;          // Raycast�� ������ �Ÿ�
    public float climbSpeed = 2f;             // Ŭ���̹� �ӵ� (����: ����/��)
    public LayerMask climbableLayer;          // Ŭ���̹� ������ ������Ʈ ���̾�

    [Header("Climbing Offsets")]
    public float climbTopOffset = 1f;         // Ŭ���̹� �Ϸ� �� �÷��̾ ������Ʈ ���� �̵��ϱ� ���� y�� ������
    public float climbCenterOffset = 1f;      // Ŭ���̹� �Ϸ� �� �÷��̾ ������Ʈ �߾����� �̵��ϱ� ���� ���� ������

    private Rigidbody rb;
    private bool isClimbing = false;          // ���� Ŭ���̹� ������ ����
    private Vector3 climbTargetPosition;       // Ŭ���̹� ��ǥ ��ġ
    private Vector3 climbCenterPosition;       // Ŭ���̹� �Ϸ� �� �߾� ��ġ
    private Vector3 climbDirection;            // Climb direction based on hit.normal

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Ŭ���̹� ���� ���� Ŭ���̹� �Է��� ����
        if (isClimbing)
            return;

        // �����̽��ٸ� ������ �� Ŭ���̹� �õ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttemptClimb();
        }
    }

    void AttemptClimb()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 1f; // �÷��̾��� �ణ �� ��ġ
        Vector3 rayDirection = transform.right;                // �÷��̾��� ������ ����
        float rayDistance = climbDistance;

        // ������� ���� Raycast �ð�ȭ (1�� ���� �ʷϻ� �� ǥ��)
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green, 1f);
        Debug.Log($"Attempting Climb: Origin={rayOrigin}, Direction={rayDirection}, Distance={rayDistance}");

        // Raycast�� ���� Ŭ���̹� ������ ������Ʈ ����
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, climbableLayer))
        {
            Debug.Log("Climbable object detected: " + hit.collider.gameObject.name);

            // Climb direction is the normal of the hit surface
            climbDirection = hit.normal;

            // Ŭ���̹� ��ǥ ��ġ ��� (������Ʈ�� ��� �߾� + climbTopOffset)
            Bounds bounds = hit.collider.bounds;
            float targetY = bounds.max.y + climbTopOffset;
            climbTargetPosition = new Vector3(bounds.center.x, targetY, bounds.center.z);

            // Ŭ���̹� �Ϸ� �� �߾� ��ġ ���� (������Ʈ �߾� �������� climbCenterOffset ��ŭ �̵�)
            climbCenterPosition = climbTargetPosition + climbDirection * climbCenterOffset;

            // Ŭ���̹� ����
            StartCoroutine(ClimbToPosition(climbTargetPosition, climbCenterPosition));
        }
        else
        {
            Debug.Log("No climbable object detected.");
        }
    }

    IEnumerator ClimbToPosition(Vector3 targetPos, Vector3 centerPos)
    {
        isClimbing = true;

        Debug.Log("Climbing started.");

        // Rigidbody�� �߷� ��Ȱ��ȭ
        rb.useGravity = false;

        // Ŭ���̹� ���� �ӵ� �ʱ�ȭ
        rb.velocity = Vector3.zero;

        // Ŭ���̹� ���� - ù ��° �ܰ�: Climb to targetPos (������Ʈ ������� �̵�)
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            Vector3 direction = (targetPos - transform.position).normalized;
            Vector3 movement = direction * climbSpeed * Time.deltaTime;

            // ������Ʈ ����
            if (Vector3.Distance(transform.position, targetPos) < movement.magnitude)
            {
                movement = targetPos - transform.position;
            }

            rb.MovePosition(transform.position + movement);

            yield return null;
        }

        // ��Ȯ�� ��ǥ ��ġ�� ����
        rb.MovePosition(targetPos);

        // Ŭ���̹� ���� - �� ��° �ܰ�: Move to centerPos (������Ʈ �߾� ���� �̵�)
        while (Vector3.Distance(transform.position, centerPos) > 0.1f)
        {
            Vector3 direction = (centerPos - transform.position).normalized;
            Vector3 movement = direction * climbSpeed * Time.deltaTime;

            // ������Ʈ ����
            if (Vector3.Distance(transform.position, centerPos) < movement.magnitude)
            {
                movement = centerPos - transform.position;
            }

            rb.MovePosition(transform.position + movement);

            yield return null;
        }

        // ��Ȯ�� �߾� ��ġ�� ����
        rb.MovePosition(centerPos);

        // Ŭ���̹� �Ϸ�
        rb.useGravity = true;
        isClimbing = false;

        Debug.Log("Climbing finished.");
    }

    private void OnDrawGizmosSelected()
    {
        // �����Ϳ��� ���� �� Raycast �ð�ȭ
        Gizmos.color = Color.green;
        Vector3 rayOrigin = transform.position + Vector3.up * 1f;
        Vector3 rayDirection = transform.right; // Raycast ������ transform.right�� ��ġ
        Gizmos.DrawRay(rayOrigin, rayDirection * climbDistance);
    }
}
