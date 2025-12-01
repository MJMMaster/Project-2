using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    void Update()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreUI: scoreText is NOT assigned in inspector!");
            return;
        }

        // If ScoreManager doesn't exist yet, prevent crash
        if (ScoreManager.Instance == null)
        {
            return;
        }

        scoreText.text = "Score: " + ScoreManager.Instance.currentScore;
    }
}