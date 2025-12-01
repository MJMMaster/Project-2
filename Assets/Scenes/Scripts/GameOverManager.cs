using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    private void Awake()
    {
        Instance = this;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TriggerGameOver()
    {
        Time.timeScale = 0f; // Pause gameplay

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + ScoreManager.Instance.currentScore;
        if (highScoreText != null)
            highScoreText.text = "High Score: " + ScoreManager.Instance.highScore;
    }

    // Button: Return to Main Menu
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        ResetAllGameData();
        SceneManager.LoadScene("MainMenu");
    }

    // Button: Quit Game
    public void QuitToDesktop()
    {
        Application.Quit();
    }

    private void ResetAllGameData()
    {
        // Reset any global game state if needed
        ScoreManager.Instance.ResetScore();
        GameManager.Instance.ResetLives(); // You’ll need a method to reset lives
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}