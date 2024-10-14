using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportDestination; // �ڷ���Ʈ�� ��ġ
    public float interactionDistance = 3f; // ��ȣ�ۿ� �Ÿ�
    public LayerMask doorLayer; // �� ���̾ ����

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // V Ű�� �������� Ȯ��
        {
            if (IsDoorInFront()) // ���� �տ� �ִ��� Ȯ��
            {
                Debug.Log("���� �����߽��ϴ�. �ڷ���Ʈ�մϴ�."); // ���� �����Ǿ��� ��
                Portal();
            }
            else
            {
                Debug.Log("���� �������� �ʾҽ��ϴ�."); // ���� �������� �ʾ��� ��
            }
        }
    }

    bool IsDoorInFront()
    {
        RaycastHit hit;
        // �÷��̾ ���� �������� Raycast�� �� (������ �Ǵ� ���� ��������)
        Vector3 direction = transform.right; // ������ �������� Raycast �߻�
        if (Physics.Raycast(transform.position, direction, out hit, interactionDistance, doorLayer))
        {
            Debug.Log("Raycast�� ���� ������ϴ�: " + hit.collider.name);
            return true;
        }
        Debug.Log("Raycast�� �ƹ��͵� ������ ���߽��ϴ�.");
        return false;
    }

    void Portal()
    {
        // ���� ��ġ�� �ڷ���Ʈ ��ġ�� ����
        transform.position = teleportDestination.position;
    }
}
