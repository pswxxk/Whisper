using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    AudioControl m_AudioControl;
    bool canMove = true;
    bool isCrouching = false; // 토글 상태를 추적할 변수

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioControl = FindObjectOfType<AudioControl>();
    }

    void FixedUpdate()
    {
        // 오디오가 재생 중일 때 움직임을 막음
        if (m_AudioControl.IsAudioPlaying())
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (!canMove)
        {
            m_Animator.SetBool("IsWalking", false);
            return;
        }

        // 플레이어의 입력값을 가져오기
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        // 걷기 상태 설정
        m_Animator.SetBool("IsWalking", isWalking);

        // 앉기 상태 전환
        if (isCrouching)
        {
            // 현재 상태가 Crouch 상태라면, Control 키를 누르면 Idle로 전환
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = false; // 웅크린 상태 해제
                m_Animator.SetBool("IsCrouching", false); // 애니메이터에서 Crouching 상태 해제
                m_Animator.SetBool("IsWalking", false); // Idle 상태로 전환
            }
        }
        else
        {
            // Ctrl 키를 눌렀을 때 앉는 상태로 전환
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = true; // 웅크린 상태로 전환
                m_Animator.SetBool("IsCrouching", true); // 애니메이터에서 Crouching 상태로 전환
            }
        }

        // 앉은 상태에서 걷기 상태 전환
        if (isCrouching && isWalking)
        {
            m_Animator.SetBool("IsCrouchWalking", true); // CrouchWalk 상태로 전환
        }
        else
        {
            m_Animator.SetBool("IsCrouchWalking", false);
        }

        // 발소리 오디오 재생 (걷거나 앉아있을 때 오디오 재생)
        if (isWalking || isCrouching)
        {
            m_AudioControl.PlayYellAudio();
        }

        // 회전 로직 (앉은 상태에서도 적용)
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        // canMove가 true일 때만 캐릭터 움직임 적용
        if (canMove)
        {
            // CrouchWalk 상태에서도 움직일 수 있도록 m_Movement를 사용하여 이동
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
            m_Rigidbody.MoveRotation(m_Rotation);
        }
    }
}
