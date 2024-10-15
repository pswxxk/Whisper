using UnityEngine;
using System.Collections;

public class MonsterTrigger : MonoBehaviour
{
    private Collider monsterCollider;  

    [SerializeField, Tooltip("활성화/비활성화 간격(초 단위)")]
    private float interval = 10f;

    void Start()
    {
        monsterCollider = GetComponent<Collider>();

        if (monsterCollider != null)
        {
            StartCoroutine(ToggleCollider());
        }
        else
        {
            Debug.LogError("Collider component is missing on this GameObject.");
        }
    }

    IEnumerator ToggleCollider()
    {
        while (true)
        {

            monsterCollider.enabled = true;
            Debug.Log("Collider 활성화됨.");
            yield return new WaitForSeconds(interval);  

            monsterCollider.enabled = false;
            Debug.Log("Collider 비활성화됨.");
            yield return new WaitForSeconds(interval);  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 콜라이더에 감지됨!");
            Destroy(other.gameObject); 
        }
    }
}
