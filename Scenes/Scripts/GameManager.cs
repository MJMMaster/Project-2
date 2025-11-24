using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    public int score = 0;

    [Header("Player Spawning")]
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public GameObject currentPlayer;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SpawnPlayer();
    }

    // -------------------------
    // SCORE SYSTEM
    // -------------------------
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    // -------------------------
    // PLAYER SPAWNING
    // -------------------------
    public void SpawnPlayer()
    {
        // Make sure any previous player is gone
        if (currentPlayer != null)
            Destroy(currentPlayer);

        currentPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(3f); // wait 3 seconds
        SpawnPlayer();
    }

    // -------------------------
    // PLAYER DIED
    // -------------------------
    public void PlayerDied()
    {
        Debug.Log("Player died! Respawning...");
        RespawnPlayer();
    }
}