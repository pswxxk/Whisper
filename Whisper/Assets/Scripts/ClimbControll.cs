using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbControll : MonoBehaviour
{
    [Header("Climbing Settings")]
    public float climbDistance = 2f;          // Raycast�� ������ �Ÿ�
    public float climbSpeed = 2f;             // Ŭ���̹� �ӵ�
    public LayerMask climbableLayer;          // Ŭ���̹� ������ ������Ʈ ���̾�

    private Rigidbody rb;
    private bool isClimbing = false;          // ���� Ŭ���̹� ������ ����
    private Vector3 climbTargetPosition;       // Ŭ���̹� ��ǥ ��ġ
    private Coroutine climbingCoroutine;       // Ŭ���̹� �ڷ�ƾ ����

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
        Vector3 rayDirection = transform.right;                // �÷��̾��� �� ����
        float rayDistance = climbDistance;

        // ������� ���� Raycast �ð�ȭ
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green, 1f);

        // Raycast�� ���� Ŭ���̹� ������ ������Ʈ ����
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, climbableLayer))
        {
            Debug.Log("Climbable object detected: " + hit.collider.gameObject.name);
            // Ŭ���̹� ��ǥ ��ġ ��� (������Ʈ�� ��� + Ŭ���̹� ����)
            Bounds bounds = hit.collider.bounds;
            climbTargetPosition = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);

            // Ŭ���̹� ����
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

        // Rigidbody�� �߷� ��Ȱ��ȭ
        rb.useGravity = false;

        // Ŭ���̹� ���� �ӵ� �ʱ�ȭ
        rb.velocity = Vector3.zero;

        

        // Ŭ���̹� ����
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

        // ��Ȯ�� ��ġ�� ����
        rb.MovePosition(targetPos);

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
        Vector3 rayDirection = transform.forward;
        Gizmos.DrawRay(rayOrigin, rayDirection * climbDistance);
    }
}
