using UnityEngine;
using System.Collections;

public class ShieldRotator : MonoBehaviour
{
    [SerializeField, Tooltip("Angular velocity in degrees per second")]
    Vector3 m_Velocity;

    bool m_IsRotating = true; // ȸ�� ���θ� ��Ÿ���� ���� �߰�
    Coroutine m_RotationCoroutine;

    void Start()
    {
        m_RotationCoroutine = StartCoroutine(RotateShield());
    }

    IEnumerator RotateShield()
    {
        while (true)
        {
            // ȸ��
            while (m_IsRotating)
            {
                transform.Rotate(m_Velocity * Time.deltaTime);

                if (Mathf.Abs(transform.eulerAngles.z) >= 90f)
                {
                    m_IsRotating = false;
                }
                yield return null; // ���� �����ӱ��� ���
            }

            // 10�� ���
            yield return new WaitForSeconds(10f);

            // 0���� ȸ��
            while (transform.eulerAngles.z > 0)
            {
                transform.Rotate(-m_Velocity * Time.deltaTime);
                yield return null; // ���� �����ӱ��� ���
            }

            // Z�� ������ 0�� �Ǹ� ����
            transform.eulerAngles = Vector3.zero;

            // 5�� ���
            yield return new WaitForSeconds(5f);

            // �ٽ� ȸ�� ����
            m_IsRotating = true;
        }
    }
}
