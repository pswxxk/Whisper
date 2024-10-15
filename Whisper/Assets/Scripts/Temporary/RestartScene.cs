using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneReloader : MonoBehaviour
{
    [SerializeField, Tooltip("이름을 설정할 씬")]
    private string sceneName; 

    void Update()
    {
        // R 키가 눌렸을 때 씬을 다시 로드
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
