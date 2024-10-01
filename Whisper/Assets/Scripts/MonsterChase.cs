using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public Transform player;  
    public float chaseDistance = 10f; 
    public float moveSpeed = 5f; 

    private bool isChasing = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
        if (isChasing)
        {
            ChasePlayer();
        }
    }
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;  
        transform.position += direction * moveSpeed * Time.deltaTime;  
    }
}
