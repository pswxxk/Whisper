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
                Portal();
            }
        }
    }

    bool IsDoorInFront()
    {
        // ī�޶󿡼� ������ Raycast�� ��� ���� �ִ��� Ȯ��
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.right, out hit, interactionDistance, doorLayer))
        {
            return true; // ���� �տ� ����
        }
        return false; // ���� ����
    }

    void Portal()
    {
        // ���� ��ġ�� �ڷ���Ʈ ��ġ�� ����
        transform.position = teleportDestination.position;
    }
}
