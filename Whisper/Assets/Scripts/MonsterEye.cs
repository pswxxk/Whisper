using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEye : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;
    bool m_IsObserving = true;
    public float openEyes = 10f;
    public float closedEyes = 5f;

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
        if (m_IsObserving && m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
    IEnumerator ToggleObserving()
    {
        while (true)
        {
            m_IsObserving = true;
            Debug.Log("´« ¶¹Áö·Õ");
            yield return new WaitForSeconds(openEyes);

            m_IsObserving = false;
            Debug.Log("´« °¨¾ÒÁö·Õ");
            yield return new WaitForSeconds(closedEyes);
        }
    }
}
