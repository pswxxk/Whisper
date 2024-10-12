using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float jumpForce = 5f; // 점프 힘 추가
    public LayerMask groundLayer; // Ground 체크용 레이어

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    AudioControl m_AudioControl;
    bool canMove = true;
    bool isCrouching = false; 
    bool isGrounded; 
    bool isJumping = false; 

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioControl = FindObjectOfType<AudioControl>();
    }

    void Update()
    {
        // 바닥 체크
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * 0.1f, 0.2f, groundLayer);

        // 바닥에 닿았으면 점프 상태 해제
        if (isGrounded && isJumping)
        {
            isJumping = false;
            m_Animator.SetBool("IsJumping", false);
        }

        // 점프 처리
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
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
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = false;
                m_Animator.SetBool("IsCrouching", false);
                m_Animator.SetBool("IsWalking", false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = true;
                m_Animator.SetBool("IsCrouching", true);
            }
        }

        // 앉은 상태에서 걷기 상태 전환
        if (isCrouching && isWalking)
        {
            m_Animator.SetBool("IsCrouchWalking", true);
        }
        else
        {
            m_Animator.SetBool("IsCrouchWalking", false);
        }

        // 발소리 오디오 재생
        if (isWalking || isCrouching)
        {
            m_AudioControl.PlayYellAudio();
        }

        // 회전 로직
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        if (canMove)
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
            m_Rigidbody.MoveRotation(m_Rotation);
        }
    }

    void Jump()
    {
        isJumping = true; // 점프 상태로 전환
        m_Animator.SetBool("IsJumping", true); // 점프 애니메이션 활성화
        m_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 점프 적용
    }
}
