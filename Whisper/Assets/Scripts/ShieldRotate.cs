using UnityEngine;
using System.Collections;

public class ShieldRotator : MonoBehaviour
{
    [SerializeField, Tooltip("Angular velocity in degrees per second")]
    Vector3 m_Velocity;

    bool m_IsRotating = true; // 회전 여부를 나타내는 변수 추가
    Coroutine m_RotationCoroutine;

    void Start()
    {
        m_RotationCoroutine = StartCoroutine(RotateShield());
    }

    IEnumerator RotateShield()
    {
        while (true)
        {
            // 회전
            while (m_IsRotating)
            {
                transform.Rotate(m_Velocity * Time.deltaTime);

                if (Mathf.Abs(transform.eulerAngles.z) >= 90f)
                {
                    m_IsRotating = false;
                }
                yield return null; // 다음 프레임까지 대기
            }

            // 10초 대기
            yield return new WaitForSeconds(10f);

            // 0도로 회전
            while (transform.eulerAngles.z > 0)
            {
                transform.Rotate(-m_Velocity * Time.deltaTime);
                yield return null; // 다음 프레임까지 대기
            }

            // Z축 각도가 0이 되면 정지
            transform.eulerAngles = Vector3.zero;

            // 5초 대기
            yield return new WaitForSeconds(5f);

            // 다시 회전 시작
            m_IsRotating = true;
        }
    }
}
