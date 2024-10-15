using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootStep : MonoBehaviour
{
    private AudioSource audioSource; // 오디오 소스 참조
    private Vector3 lastPosition; // 이전 위치 저장

    void Start()
    {
        // 오디오 소스 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position; // 초기 위치 저장
    }

    void Update()
    {
        // 현재 위치와 이전 위치를 비교하여 이동 여부 확인
        if (transform.position != lastPosition)
        {
            // 위치가 변경된 경우, 오디오 소스 활성화
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // 오디오 소스 재생
            }
        }
        else
        {
            // 위치가 변경되지 않은 경우, 오디오 소스 비활성화
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // 오디오 소스 정지
            }
        }

        // 이전 위치 업데이트
        lastPosition = transform.position;
    }
}
