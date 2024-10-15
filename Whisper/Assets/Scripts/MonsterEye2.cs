using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEye2 : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange; // 플레이어가 콜라이더 안에 있는지 여부
    bool m_IsObserving = true;
    public float openEyes = 5f;
    public float closedEyes = 5f;
    public LayerMask obstacleMask;  // 장애물 레이어 마스크
    public Color rayColor = Color.red;  // Ray 색상
    public float rayLength = 100f; // Ray 길이

    void Start()
    {
        StartCoroutine(ToggleObserving());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        // 눈을 뜨고 있고 플레이어가 콜라이더 안에 있을 때만 Ray 발사
        if (m_IsObserving && m_IsPlayerInRange)
        {
            // 플레이어와 몬스터 사이의 방향 계산
            Vector3 direction = (player.position - transform.position).normalized;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            // Raycast를 시각적으로 표시
            Debug.DrawRay(ray.origin, ray.direction * rayLength, rayColor);

            // 플레이어와 몬스터 사이에 장애물이 있는지 Raycast로 확인
            if (Physics.Raycast(ray, out raycastHit, rayLength, obstacleMask | LayerMask.GetMask("Player")))
            {
                if (raycastHit.collider.transform == player)
                {
                    // 장애물이 없으면 플레이어를 잡음
                    gameEnding.CaughtPlayer();
                }
                else
                {
                    // 장애물이 있으면 플레이어를 인식하지 못함
                    Debug.Log("플레이어가 장애물 뒤에 있습니다.");
                }
            }
        }
    }

    IEnumerator ToggleObserving()
    {
        while (true)
        {
            m_IsObserving = true;
            Debug.Log("눈 떴지롱");
            yield return new WaitForSeconds(openEyes);

            m_IsObserving = false;
            Debug.Log("눈 감았지롱");
            yield return new WaitForSeconds(closedEyes);
        }
    }
}
