using System.Collections;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource yellAudioSource; 
    public float delayTime = 10f; // 10�� ������ �ð�
    private bool canPlay = true; 

    public bool IsAudioPlaying()
    {
        return yellAudioSource.isPlaying;
    }

    // Ư�� ������� ����ϰ� ������ �����ϴ� �޼���
    public void PlayYellAudio()
    {
        if (canPlay && !yellAudioSource.isPlaying)
        {
            yellAudioSource.Play();
            StartCoroutine(HandleAudioDelay());
        }
    }

    IEnumerator HandleAudioDelay()
    {
        canPlay = false;

        // ������� ���� ������ ���
        yield return new WaitForSeconds(yellAudioSource.clip.length);

        // �߰��� 10�� ���� ���
        yield return new WaitForSeconds(delayTime);

        // �ٽ� ������� ����� �� �ֵ��� ����
        canPlay = true;
    }
}