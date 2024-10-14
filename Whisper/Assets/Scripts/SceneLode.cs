using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLode : MonoBehaviour
{
    private Renderer objectRenderer;
    public Text interactionText; // 문구를 표시할 UI 텍스트
    private bool isHovering = false; // 현재 호버 상태인지 여부

    private Color[] originalColors; // 모든 재질의 원래 색상을 저장할 배열

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // 모든 재질의 원래 색상을 저장
        int materialCount = objectRenderer.materials.Length;
        originalColors = new Color[materialCount];
        for (int i = 0; i < materialCount; i++)
        {
            originalColors[i] = objectRenderer.materials[i].color;
        }

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

    private void OnTriggerStay(Collider other)
    {
        if (isHovering && Input.GetKeyDown(KeyCode.V))
        {
            GameManager.Instance.LoadScene("SceneName");
            Debug.Log("어쨋든 작동은 됨");
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
            // 모든 재질의 색상을 초록색으로 변경
            foreach (var mat in objectRenderer.materials)
            {
                mat.color = Color.green;
            }
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = "V키를 눌러 다음 방으로 이동";
            }
        }
        else
        {
            // 모든 재질의 색상을 원래대로 복원
            for (int i = 0; i < objectRenderer.materials.Length; i++)
            {
                objectRenderer.materials[i].color = originalColors[i];
            }
            if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(false);
                }
        }
    }
}
