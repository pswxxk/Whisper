using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLode : MonoBehaviour
{
    private Renderer objectRenderer;
    public Text interactionText; // ������ ǥ���� UI �ؽ�Ʈ
    private bool isHovering = false; // ���� ȣ�� �������� ����

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // interactionText�� �����Ϳ��� ����� ������� �ʾ��� ��� ���
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // ó������ ���� ��Ȱ��ȭ
        }
        else
        {
            Debug.LogError("InteractionText UI�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    // �÷��̾ Ʈ���� �ݶ��̴��� ������ �� ȣ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" �±װ� �ִ� ������Ʈ�� �浹�� ���
        {
            Hover(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isHovering && Input.GetKeyDown(KeyCode.V))
        {
            GameManager.Instance.LoadScene("SceneName");
            Debug.Log("��¶�� �۵��� ��");
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
        this.isHovering = isHovering;

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
