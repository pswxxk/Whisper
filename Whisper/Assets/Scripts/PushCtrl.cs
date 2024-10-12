using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCtrl : MonoBehaviour
{
    public float maxDistance = 2f;  // �� �� �ִ� �ִ� �Ÿ�
    public Transform holdPoint;     // �÷��̾ ������Ʈ�� ���� ��ġ

    private GameObject objectToPull;
    private bool isPulling = false;
    private Rigidbody objectRb;
    private FixedJoint fixedJoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isPulling)
            {
                Debug.Log("E Ű�� ����: ������Ʈ�� �����ϴ�.");
                ReleaseObject();
            }
            else
            {
                Debug.Log("E Ű�� ����: ������Ʈ�� �� �õ�.");
                TryToPullObject();
            }
        }
    }

    void TryToPullObject()
    {
        RaycastHit hit;

        // Raycast �ð������� Ȯ���ϱ� (���� ���� ������Ʈ�� ���ϴ��� Ȯ��)
        Debug.DrawRay(transform.position, transform.right * maxDistance, Color.red, 1f);

        if (Physics.Raycast(transform.position, transform.right, out hit, maxDistance))
        {
            Debug.Log("Raycast�� ������Ʈ�� �浹: " + hit.collider.name);

            if (hit.collider.CompareTag("Push"))
            {
                Debug.Log("Pullable �±׸� ���� ������Ʈ �߰�: " + hit.collider.name);
                objectToPull = hit.collider.gameObject;
                objectRb = objectToPull.GetComponent<Rigidbody>();

                if (objectRb != null)
                {
                    Debug.Log("������Ʈ�� Rigidbody�� ����.");
                    objectRb.useGravity = false;  // �߷��� ��Ȱ��ȭ
                    objectRb.drag = 10f;          // ��ü�� �� �� ���� �߰�

                    // FixedJoint�� ������ ���� ����
                    fixedJoint = objectToPull.AddComponent<FixedJoint>();
                    fixedJoint.connectedBody = GetComponent<Rigidbody>();  // �÷��̾�� ����
                    isPulling = true;
                }
                else
                {
                    Debug.Log("������Ʈ�� Rigidbody�� ����.");
                }
            }
            else
            {
                Debug.Log("�±װ� Push�� �ƴ�.");
            }
        }
        else
        {
            Debug.Log("Raycast�� �ƹ� ������Ʈ�� ������ ����.");
        }
    }

    void ReleaseObject()
    {
        isPulling = false;

        if (objectRb != null)
        {
            objectRb.useGravity = true;  // �߷� �ٽ� Ȱ��ȭ
            objectRb.drag = 1f;          // ���� ������ ����

            // FixedJoint ����
            if (fixedJoint != null)
            {
                Destroy(fixedJoint);     // Joint �����Ͽ� ������ ���� ����
                Debug.Log("FixedJoint ����");
            }

            objectToPull = null;
            objectRb = null;
        }
    }
}
