using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportDestination; // 텔레포트할 위치
    public float interactionDistance = 3f; // 상호작용 거리
    public LayerMask doorLayer; // 문 레이어를 지정

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // V 키를 눌렀는지 확인
        {
            if (IsDoorInFront()) // 문이 앞에 있는지 확인
            {
                Debug.Log("문을 감지했습니다. 텔레포트합니다."); // 문이 감지되었을 때
                Portal();
            }
            else
            {
                Debug.Log("문이 감지되지 않았습니다."); // 문이 감지되지 않았을 때
            }
        }
    }

    bool IsDoorInFront()
    {
        RaycastHit hit;
        // 플레이어가 보는 방향으로 Raycast를 쏨 (오른쪽 또는 왼쪽 방향으로)
        Vector3 direction = transform.right; // 오른쪽 방향으로 Raycast 발사
        if (Physics.Raycast(transform.position, direction, out hit, interactionDistance, doorLayer))
        {
            Debug.Log("Raycast가 문을 맞췄습니다: " + hit.collider.name);
            return true;
        }
        Debug.Log("Raycast가 아무것도 맞추지 못했습니다.");
        return false;
    }

    void Portal()
    {
        // 현재 위치를 텔레포트 위치로 설정
        transform.position = teleportDestination.position;
    }
}
