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

        // 토글 방식으로 Ctrl 키를 눌렀을 때 앉거나 일어서는 상태 전환
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            isCrouching = !isCrouching; // 토글 상태 전환
        }

        // 애니메이터에서 앉는 상태 또는 서는 상태 전환
        if (isCrouching)
        {
            m_Animator.SetBool("IsCrouching", true); // StandingToCrouch 상태로 전환
        }
        else
        {
            m_Animator.SetBool("IsCrouching", false); // CrouchToStanding 상태로 전환
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
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
            m_Rigidbody.MoveRotation(m_Rotation);
        }
    }
}
