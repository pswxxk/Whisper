using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpupControl : MonoBehaviour
{
    // �÷��̾ Ʈ���޸��� ����� �� ����� �߰� ���� ��
    public float bounceForce = 10f;

    // Ʈ���޸��� �ݶ��̴����� �߻��ϴ� �浹�� ����
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� Rigidbody�� �ִ��� Ȯ��
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        // �浹�� ��ü�� �÷��̾����� Ȯ���ϰ�, Rigidbody�� ������ ƨ�⵵�� ó��
        if (rb != null && collision.collider.CompareTag("Player"))
        {
            // �Ʒ� ���⿡�� Ʈ���޸��� ��Ҵ��� Ȯ�� (�÷��̾ ������ �������� ����)
            if (collision.relativeVelocity.y <= 0)
            {
                // Ʈ���޸� ȿ��: Rigidbody�� �߰����� ���� �������� ����
                Vector3 bounce = Vector3.up * bounceForce;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);  // ���� Y�� �ӵ� ����
                rb.AddForce(bounce, ForceMode.Impulse);  // ���� ���������� ����
            }
        }
    }
}
