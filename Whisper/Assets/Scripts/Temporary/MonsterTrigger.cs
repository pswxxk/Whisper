using UnityEngine;
using System.Collections;

public class MonsterTrigger : MonoBehaviour
{
    private Collider monsterCollider;  

    [SerializeField, Tooltip("Ȱ��ȭ/��Ȱ��ȭ ����(�� ����)")]
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
            Debug.Log("Collider Ȱ��ȭ��.");
            yield return new WaitForSeconds(interval);  

            monsterCollider.enabled = false;
            Debug.Log("Collider ��Ȱ��ȭ��.");
            yield return new WaitForSeconds(interval);  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ �ݶ��̴��� ������!");
            Destroy(other.gameObject); 
        }
    }
}
