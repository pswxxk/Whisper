using UnityEngine;
using System.Collections;

public class PointLight : MonoBehaviour
{
    private Light pointLight; 

    [SerializeField, Tooltip("Light on/off interval in seconds")]
    private float interval = 10f; // 10초 간격

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

    // 라이트 on/off를 반복하는 코루틴
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
