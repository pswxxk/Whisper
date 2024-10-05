using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpupControl : MonoBehaviour
{
    // 플레이어가 트램펄린을 밟았을 때 적용될 추가 점프 힘
    public float bounceForce = 10f;

    // 트램펄린의 콜라이더에서 발생하는 충돌을 감지
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체에 Rigidbody가 있는지 확인
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        // 충돌한 객체가 플레이어인지 확인하고, Rigidbody가 있으면 튕기도록 처리
        if (rb != null && collision.collider.CompareTag("Player"))
        {
            // 아래 방향에서 트램펄린을 밟았는지 확인 (플레이어가 위에서 내려왔을 때만)
            if (collision.relativeVelocity.y <= 0)
            {
                // 트램펄린 효과: Rigidbody에 추가적인 힘을 위쪽으로 가함
                Vector3 bounce = Vector3.up * bounceForce;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);  // 기존 Y축 속도 제거
                rb.AddForce(bounce, ForceMode.Impulse);  // 힘을 순간적으로 가함
            }
        }
    }
}
