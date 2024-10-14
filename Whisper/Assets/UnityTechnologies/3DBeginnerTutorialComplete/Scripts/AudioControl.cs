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

    // 특정 오디오가 재생 중인지 확인하는 메서드
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

    // 게임 시작 후 10초 뒤에 오디오 재생을 위한 코루틴
    IEnumerator StartDelayedAudio()
    {
        yield return new WaitForSeconds(10f); 
        PlayYellAudio(); 
    }

    // 오디오가 끝난 후 딜레이를 처리하는 코루틴
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
