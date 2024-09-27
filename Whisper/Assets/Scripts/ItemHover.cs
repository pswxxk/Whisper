using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHover : MonoBehaviour
{
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    // 플레이어가 트리거 콜라이더에 들어왔을 때 호출됨
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // "Player" 태그가 있는 오브젝트와 충돌할 경우
        {
            Hover(true);
        }
    }

    // 플레이어가 트리거 콜라이더에서 나갔을 때 호출됨
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Hover(false);
        }
    }

    // 호버 상태를 켜거나 끄는 함수
    void Hover(bool isHovering)
    {
        if (isHovering)
        {
            objectRenderer.material.color = Color.green; // 호버 상태일 때 색상 변경
        }
        else
        {
            objectRenderer.material.color = Color.white; // 호버 상태 아닐 때 기본 색상
        }
    }
}
