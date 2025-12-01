using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Lives System")]
    public int maxLives = 3;
    public int currentLives;

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

        currentLives = maxLives;
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
        // wait 3 seconds before respawn
        Instance.StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(3f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // reset position to center of map
            player.transform.position = new Vector3(0f, 25f, 0f);

            // reset health
            Health health = player.GetComponent<Health>();
            if (health != null)
            {
                health.ResetHealth();
            }
        }
    }

    // -------------------------
    // PLAYER DIED
    // -------------------------
    public void ResetLives()
    {
        score = 0;
        currentLives = maxLives;
        currentPlayer = null;
    }

    public void ResetScore()
    {
        score = 0;
    }
    public void PlayerDied()
    {
        Debug.Log("PLAYER DIED. Lives before decrement = " + currentLives);

        currentLives--;

        Debug.Log("Lives now = " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("GAME OVER SHOULD HAPPEN NOW");
            GameOverManager.Instance.TriggerGameOver();
        }
        else
        {
            Debug.Log("Respawning player. Lives left = " + currentLives);
            RespawnPlayer();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject); // Destroy the singleton so it doesn't carry broken references
        }
    }
}