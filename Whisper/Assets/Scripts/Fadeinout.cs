using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fadeinout : MonoBehaviour
{
    public bool isFadeIn; // true �϶� FadeIn false �ϋ� FadeOut �Դϴ�.
    public GameObject panel; // ���� ����� Canvas �Դϴ�.
    private Action onCompleteCallback; // ���� ���Լ��� ������ �ٷ� �ؾ��� ������ �ִٸ� ���⿡ Action�� �Է��� �ּ���.
    float totalTime = 0f;
    public float fadedTime = 0.5f;
    void Start()
    {
        if (!panel)
        {
            Debug.LogError("panel ������Ʈ ����.");
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
        Debug.Log("Fadeout ����");
        StartCoroutine(CoFadeOut());
        Debug.Log("Fadeout ��");
    }

    IEnumerator CoFadeIn()
    {

        while (totalTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, totalTime / fadedTime));

            totalTime += Time.deltaTime;
            Debug.Log("Fade In ��...");
            yield return null;
        }

        Debug.Log("Fade In ��");
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
            Debug.Log("Fade Out ��...");
            yield return null;
        }

        Debug.Log("Fade In ��");
        panel.SetActive(false);
        onCompleteCallback?.Invoke();
        yield break;
    }


    public void RegisterCallback(Action callback)
    {
        onCompleteCallback = callback;
    }

}
