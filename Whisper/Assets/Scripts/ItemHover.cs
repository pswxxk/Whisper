using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemHover : MonoBehaviour
{
    private Renderer objectRenderer;
    public Text interactionText; // 문구를 표시할 UI 텍스트
    private bool isHovering = false; // 현재 호버 상태인지 여부

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // interactionText가 에디터에서 제대로 연결되지 않았을 경우 대비
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // 처음에는 문구 비활성화
        }
        else
        {
            Debug.LogError("InteractionText UI가 할당되지 않았습니다.");
        }
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
        this.isHovering = isHovering;

        if (isHovering)
        {
            objectRenderer.material.color = Color.green; // 호버 상태일 때 색상 변경
        }
        else
        {
            objectRenderer.material.color = Color.white; // 호버 상태 아닐 때 기본 색상

            // interactionText를 비활성화
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        // 호버 상태일 때 E 키가 눌리면 문구 출력
        if (isHovering && Input.GetKeyDown(KeyCode.Backspace))
        {
            if (interactionText != null)
            {
                
                DisplayMessage("아주 작아진 주인공의 모습이 보인다.");
            }
        }
    }

    // 문구를 출력하는 함수
    void DisplayMessage(string message)
    {
        interactionText.text = message;
        interactionText.gameObject.SetActive(true); // 문구 활성화
    }
}
