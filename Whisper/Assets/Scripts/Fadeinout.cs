using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fadeinout : MonoBehaviour
{
    public bool isFadeIn; // true 일때 FadeIn false 일떄 FadeOut 입니다.
    public GameObject panel; // 씬에 저장될 Canvas 입니다.
    private Action onCompleteCallback; // 만약 이함수가 동작후 바로 해야할 동작이 있다면 여기에 Action을 입력해 주세요.
    float totalTime = 0f;
    public float fadedTime = 0.5f;
    void Start()
    {
        if (!panel)
        {
            Debug.LogError("panel 오브젝트 없음.");
        }

        if (isFadeIn)
        {
            panel.SetActive(true);
            StartCoroutine(CoFadeIn());
        }

        else
        {
            panel.SetActive(false);
        }
    }

    public void FadeOut()
    {
        panel.SetActive(true);
        Debug.Log("Fadeout 시작");
        StartCoroutine(CoFadeOut());
        Debug.Log("Fadeout 끝");
    }

    IEnumerator CoFadeIn()
    {

        while (totalTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, totalTime / fadedTime));

            totalTime += Time.deltaTime;
            Debug.Log("Fade In 중...");
            yield return null;
        }

        Debug.Log("Fade In 끝");
        panel.SetActive(false);
        onCompleteCallback?.Invoke();
        yield break;
    }

    IEnumerator CoFadeOut()
    {

        while (totalTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, totalTime / fadedTime));

            totalTime += Time.deltaTime;
            Debug.Log("Fade Out 중...");
            yield return null;
        }

        Debug.Log("Fade In 끝");
        panel.SetActive(false);
        onCompleteCallback?.Invoke();
        yield break;
    }


    public void RegisterCallback(Action callback)
    {
        onCompleteCallback = callback;
    }

}
