using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootStep : MonoBehaviour
{
    private AudioSource audioSource; // ����� �ҽ� ����
    private Vector3 lastPosition; // ���� ��ġ ����

    void Start()
    {
        // ����� �ҽ� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        // ���� ��ġ�� ���� ��ġ�� ���Ͽ� �̵� ���� Ȯ��
        if (transform.position != lastPosition)
        {
            // ��ġ�� ����� ���, ����� �ҽ� Ȱ��ȭ
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // ����� �ҽ� ���
            }
        }
        else
        {
            // ��ġ�� ������� ���� ���, ����� �ҽ� ��Ȱ��ȭ
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // ����� �ҽ� ����
            }
        }

        // ���� ��ġ ������Ʈ
        lastPosition = transform.position;
    }
}
