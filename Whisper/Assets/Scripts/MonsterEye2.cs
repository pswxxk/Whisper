using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEye2 : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange; // �÷��̾ �ݶ��̴� �ȿ� �ִ��� ����
    bool m_IsObserving = true;
    public float openEyes = 5f;
    public float closedEyes = 5f;
    public LayerMask obstacleMask;  // ��ֹ� ���̾� ����ũ
    public Color rayColor = Color.red;  // Ray ����
    public float rayLength = 100f; // Ray ����

    void Start()
    {
        StartCoroutine(ToggleObserving());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        // ���� �߰� �ְ� �÷��̾ �ݶ��̴� �ȿ� ���� ���� Ray �߻�
        if (m_IsObserving && m_IsPlayerInRange)
        {
            // �÷��̾�� ���� ������ ���� ���
            Vector3 direction = (player.position - transform.position).normalized;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            // Raycast�� �ð������� ǥ��
            Debug.DrawRay(ray.origin, ray.direction * rayLength, rayColor);

            // �÷��̾�� ���� ���̿� ��ֹ��� �ִ��� Raycast�� Ȯ��
            if (Physics.Raycast(ray, out raycastHit, rayLength, obstacleMask | LayerMask.GetMask("Player")))
            {
                if (raycastHit.collider.transform == player)
                {
                    // ��ֹ��� ������ �÷��̾ ����
                    gameEnding.CaughtPlayer();
                }
                else
                {
                    // ��ֹ��� ������ �÷��̾ �ν����� ����
                    Debug.Log("�÷��̾ ��ֹ� �ڿ� �ֽ��ϴ�.");
                }
            }
        }
    }

    IEnumerator ToggleObserving()
    {
        while (true)
        {
            m_IsObserving = true;
            Debug.Log("�� ������");
            yield return new WaitForSeconds(openEyes);

            m_IsObserving = false;
            Debug.Log("�� ��������");
            yield return new WaitForSeconds(closedEyes);
        }
    }
}
