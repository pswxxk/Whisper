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
            // ���Ͱ� �÷��̾��� ���鿡 �ִ��� ���� Ȯ�� (Dot Product ���)
            Vector3 directionToPlayer = (transform.position - player.position).normalized;
            float dotProduct = Vector3.Dot(player.forward, directionToPlayer);

            // Dot Product�� ����� �� �÷��̾ ���͸� ���� �ִ� ����
            if (dotProduct < 0)  // ������ �� �÷��̾ �ڸ� ���� ����
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

