using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneReloader : MonoBehaviour
{
    [SerializeField, Tooltip("�̸��� ������ ��")]
    private string sceneName; 

    void Update()
    {
        // R Ű�� ������ �� ���� �ٽ� �ε�
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }


    void ReloadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
