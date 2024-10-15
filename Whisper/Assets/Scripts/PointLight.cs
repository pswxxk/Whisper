using UnityEngine;
using System.Collections;

public class PointLight : MonoBehaviour
{
    private Light pointLight; 

    [SerializeField, Tooltip("Light on/off interval in seconds")]
    private float interval = 10f; // 10�� ����

    void Start()
    {
        pointLight = GetComponent<Light>();

        if (pointLight != null)
        {
            StartCoroutine(ToggleLight());
        }
        else
        {
            Debug.LogError("Light component is missing on this GameObject.");
        }
    }

    // ����Ʈ on/off�� �ݺ��ϴ� �ڷ�ƾ
    IEnumerator ToggleLight()
    {
        while (true)
        {
            pointLight.enabled = true; 
            yield return new WaitForSeconds(interval); 

            pointLight.enabled = false; 
            yield return new WaitForSeconds(interval); 
        }
    }
}
