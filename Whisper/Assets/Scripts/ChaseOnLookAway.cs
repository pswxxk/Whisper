using UnityEngine;

public class MonsterChaseWhenNotLooked : MonoBehaviour
{
    public Transform player;  
    public float moveSpeed = 5f;  
    public float chaseDistance = 20f;  

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < chaseDistance)
        {
            // 몬스터가 플레이어의 정면에 있는지 여부 확인 (Dot Product 사용)
            Vector3 directionToPlayer = (transform.position - player.position).normalized;
            float dotProduct = Vector3.Dot(player.forward, directionToPlayer);

            // Dot Product가 양수일 때 플레이어가 몬스터를 보고 있는 상태
            if (dotProduct < 0)  // 음수일 때 플레이어가 뒤를 보고 있음
            {
                ChasePlayer();  
            }
        }
    }
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;  
    }
}

