using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    void Update()
    {
        // เช็คว่าผู้เล่นกด Enter (หรือ KeyCode.Space ก็ได้)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
