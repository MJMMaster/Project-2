using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ResetLives();  // Make a method to reset lives, score, etc.

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScore();

        // Load the gameplay scene (replace "LevelScene" with your scene name)
        SceneManager.LoadScene("Level");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}