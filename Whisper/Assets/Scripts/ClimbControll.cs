using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbControll : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded;
    public float climbHeight = 1.5f;       // ���� ���� �ö� ����
    public float climbSpeed = 5f;          // ���� ���� �ö󰡴� �ӵ�
    public LayerMask climbableLayer;       // �ö� �� �ִ� ������Ʈ�� ���̾�

    private Rigidbody rb;
    private bool isClimbing = false;       // ���� ���ڿ� �ö󰡴� ������ ����
    private Vector3 targetPosition;        // �÷��̾ �ö� ��ǥ ��ġ

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isClimbing) // �ö󰡴� ���� �ƴ� ���� �̵� �� ���� ����
        {
            // ���� �̵� �Է�
            float move = Input.GetAxis("Horizontal");

            // �÷��̾��� �̵�
            Vector3 movement = new Vector3(move * moveSpeed, rb.velocity.y, 0);
            rb.velocity = movement;

            // ���� �ݴ�Ű�� ���� �� y�� ȸ������ 180���� ����
            if (move < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0); // ������ �ٶ󺸴� ȸ��
            }
            else if (move > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // �������� �ٶ󺸴� ȸ��
            }

            // ���� ó��
            if (Input.GetKeyDown(KeyCode.E) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        // �����̽��� ������ �� ���ڿ� �پ �ö󰡴� ���
        if (!isClimbing && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // �÷��̾� �տ� ���ڰ� �ִ��� Ȯ�� (Raycast)
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, climbableLayer))
            {
                // ���ڸ� ���������� �ö� ��ġ ���
                targetPosition = new Vector3(hit.transform.position.x, hit.transform.position.y + climbHeight, hit.transform.position.z);

                // �ڷ�ƾ �����ؼ� ���ڿ� �ö󰡱�
                StartCoroutine(Climb());
            }
        }
    }

    // ���ڿ� �ö󰡴� �ڷ�ƾ
    IEnumerator Climb()
    {
        isClimbing = true;

        // �÷��̾ ��ǥ ��ġ�� �̵��� ������ �̵� ó��
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, climbSpeed * Time.deltaTime);
            yield return null;
        }

        // ��Ȯ�� ��ġ�� ���� �� ���� ������ ����
        transform.position = targetPosition;

        // ���� ���� �ö󰡴� ���� ����
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
