using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCtrl : MonoBehaviour
{
    public float maxDistance = 2f;  // 끌 수 있는 최대 거리
    public Transform holdPoint;     // 플레이어가 오브젝트를 잡을 위치

    private GameObject objectToPull;
    private bool isPulling = false;
    private Rigidbody objectRb;
    private FixedJoint fixedJoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isPulling)
            {
                Debug.Log("E 키가 눌림: 오브젝트를 놓습니다.");
                ReleaseObject();
            }
            else
            {
                Debug.Log("E 키가 눌림: 오브젝트를 끌 시도.");
                TryToPullObject();
            }
        }
    }

    void TryToPullObject()
    {
        RaycastHit hit;

        // Raycast 시각적으로 확인하기 (빨간 선이 오브젝트로 향하는지 확인)
        Debug.DrawRay(transform.position, transform.right * maxDistance, Color.red, 1f);

        if (Physics.Raycast(transform.position, transform.right, out hit, maxDistance))
        {
            Debug.Log("Raycast가 오브젝트에 충돌: " + hit.collider.name);

            if (hit.collider.CompareTag("Push"))
            {
                Debug.Log("Pullable 태그를 가진 오브젝트 발견: " + hit.collider.name);
                objectToPull = hit.collider.gameObject;
                objectRb = objectToPull.GetComponent<Rigidbody>();

                if (objectRb != null)
                {
                    Debug.Log("오브젝트의 Rigidbody가 있음.");
                    objectRb.useGravity = false;  // 중력을 비활성화
                    objectRb.drag = 10f;          // 물체를 끌 때 마찰 추가

                    // FixedJoint로 물리적 연결 생성
                    fixedJoint = objectToPull.AddComponent<FixedJoint>();
                    fixedJoint.connectedBody = GetComponent<Rigidbody>();  // 플레이어와 연결
                    isPulling = true;
                }
                else
                {
                    Debug.Log("오브젝트에 Rigidbody가 없음.");
                }
            }
            else
            {
                Debug.Log("태그가 Push가 아님.");
            }
        }
        else
        {
            Debug.Log("Raycast가 아무 오브젝트도 맞추지 못함.");
        }
    }

    void ReleaseObject()
    {
        isPulling = false;

        if (objectRb != null)
        {
            objectRb.useGravity = true;  // 중력 다시 활성화
            objectRb.drag = 1f;          // 원래 마찰로 복구

            // FixedJoint 해제
            if (fixedJoint != null)
            {
                Destroy(fixedJoint);     // Joint 삭제하여 물리적 연결 해제
                Debug.Log("FixedJoint 해제");
            }

            objectToPull = null;
            objectRb = null;
        }
    }
}
