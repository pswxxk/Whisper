using UnityEngine;
using System.Collections;

public class ShieldRotator : MonoBehaviour
{
    [SerializeField, Tooltip("Angular velocity in degrees per second")]
    Vector3 m_Velocity;

    bool m_IsRotating = true;
    Coroutine m_RotationCoroutine;

    void Start()
    {
        m_RotationCoroutine = StartCoroutine(RotateShield());
    }

    IEnumerator RotateShield()
    {
        while (true)
        {
            while (m_IsRotating)
            {
                RotateX();

                float normalizedX = NormalizeAngle(transform.eulerAngles.x);

                if (normalizedX >= 90f)
                {
                    m_IsRotating = false;
                }

                yield return null;
            }

            yield return new WaitForSeconds(10f);

            while (NormalizeAngle(transform.eulerAngles.x) > 0f)
            {
                RotateX(-m_Velocity.x);
                yield return null;
            }

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

            yield return new WaitForSeconds(10f);

            m_IsRotating = true;
        }
    }

    void RotateX(float velocityX = 0)
    {
        Vector3 currentRotation = transform.eulerAngles;
        float deltaX = velocityX == 0 ? m_Velocity.x * Time.deltaTime : velocityX * Time.deltaTime;

        transform.eulerAngles = new Vector3(
            currentRotation.x + deltaX,
            currentRotation.y,
            currentRotation.z
        );
    }

    // 각도를 -180° ~ 180° 범위로 정규화하는 함수
    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
