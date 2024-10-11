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
                Portal();
            }
        }
    }

    bool IsDoorInFront()
    {
        // 카메라에서 앞으로 Raycast를 쏘아 문이 있는지 확인
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.right, out hit, interactionDistance, doorLayer))
        {
            return true; // 문이 앞에 있음
        }
        return false; // 문이 없음
    }

    void Portal()
    {
        // 현재 위치를 텔레포트 위치로 설정
        transform.position = teleportDestination.position;
    }
}
