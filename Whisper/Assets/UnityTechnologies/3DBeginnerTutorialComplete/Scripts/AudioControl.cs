using System.Collections;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource yellAudioSource;
    public float delayTime = 10f; 
    private bool canPlay = true;

    void Start()
    {

        StartCoroutine(StartDelayedAudio());
    }

    // Ư�� ������� ��� ������ Ȯ���ϴ� �޼���
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

    // ���� ���� �� 10�� �ڿ� ����� ����� ���� �ڷ�ƾ
    IEnumerator StartDelayedAudio()
    {
        yield return new WaitForSeconds(10f); 
        PlayYellAudio(); 
    }

    // ������� ���� �� �����̸� ó���ϴ� �ڷ�ƾ
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
