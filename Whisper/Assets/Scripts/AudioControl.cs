using System.Collections;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource yellAudioSource; 
    public float delayTime = 10f; // 10초 딜레이 시간
    private bool canPlay = true; 

    public bool IsAudioPlaying()
    {
        return yellAudioSource.isPlaying;
    }

    // 특정 오디오를 재생하고 딜레이 적용하는 메서드
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

        // 오디오가 끝날 때까지 대기
        yield return new WaitForSeconds(yellAudioSource.clip.length);

        // 추가로 10초 동안 대기
        yield return new WaitForSeconds(delayTime);

        // 다시 오디오를 재생할 수 있도록 설정
        canPlay = true;
    }
}