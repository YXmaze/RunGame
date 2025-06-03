using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("FirstScene");  // Replace with the actual name of your game scene
    }

    public void QuitGame()
    {
        Application.Quit();  // Only works in builds; shows in editor log for testing
        Debug.Log("Quit Game");
    }
}
