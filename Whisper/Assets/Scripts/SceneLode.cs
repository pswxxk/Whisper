using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLode : MonoBehaviour
{
    private Renderer objectRenderer;
    public Text interactionText; // ������ ǥ���� UI �ؽ�Ʈ
    private bool isHovering = false; // ���� ȣ�� �������� ����

    private Color[] originalColors; // ��� ������ ���� ������ ������ �迭

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // ��� ������ ���� ������ ����
        int materialCount = objectRenderer.materials.Length;
        originalColors = new Color[materialCount];
        for (int i = 0; i < materialCount; i++)
        {
            originalColors[i] = objectRenderer.materials[i].color;
        }

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
            // ��� ������ ������ �ʷϻ����� ����
            foreach (var mat in objectRenderer.materials)
            {
                mat.color = Color.green;
            }
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = "VŰ�� ���� ���� ������ �̵�";
            }
        }
        else
        {
            // ��� ������ ������ ������� ����
            for (int i = 0; i < objectRenderer.materials.Length; i++)
            {
                objectRenderer.materials[i].color = originalColors[i];
            }
            if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(false);
                }
        }
    }
}
