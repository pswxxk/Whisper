using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHover : MonoBehaviour
{
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    // �÷��̾ Ʈ���� �ݶ��̴��� ������ �� ȣ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" �±װ� �ִ� ������Ʈ�� �浹�� ���
        {
            Hover(true);
        }
    }

    // �÷��̾ Ʈ���� �ݶ��̴����� ������ �� ȣ���
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Hover(false);
        }
    }

    // ȣ�� ���¸� �Ѱų� ���� �Լ�
    void Hover(bool isHovering)
    {
        if (isHovering)
        {
            objectRenderer.material.color = Color.green; // ȣ�� ������ �� ���� ����
        }
        else
        {
            objectRenderer.material.color = Color.white; // ȣ�� ���� �ƴ� �� �⺻ ����
        }
    }
}
