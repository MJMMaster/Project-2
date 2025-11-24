using UnityEngine;
using System.Collections.Generic;

public class LevelData : MonoBehaviour
{
    public static LevelData Instance { get; private set; }

    [Header("Level Dimensions")]
    [Tooltip("Size of the playable area in meters.")]
    public Vector3 levelSize = new Vector3(100f, 100f, 100f);

    [Header("Ground Settings")]
    [Tooltip("Y position where the ground plane exists.")]
    public float groundHeight = 0f;

    [Header("Vertical Limit")]
    [Tooltip("Maximum height the player is allowed to fly.")]
    public float maxYLimit;

    [Header("Spawn Settings")]
    [Tooltip("Default spawn point for the player.")]
    public Transform playerSpawnPoint;

    [Header("Entity Prefabs")]
    public GameObject healthPackPrefab;
    public GameObject astronautPrefab;
    public GameObject ufoPrefab;

    [Header("Spawn Amounts")]
    public int numHealthPacks = 3;
    public int numAstronauts = 5;
    public int numUFOs = 2;

    [HideInInspector]
    public List<SpawnPoint> allSpawnPoints = new List<SpawnPoint>();

    [Header("Debug")]
    public bool drawBounds = true;

    private void Awake()
    {
        Instance = this;
        CollectSpawnPoints();
    }

    void CollectSpawnPoints()
    {
        allSpawnPoints.Clear();

        SpawnPoint[] found = GetComponentsInChildren<SpawnPoint>();

        foreach (var sp in found)
        {
            if (sp.active)
                allSpawnPoints.Add(sp);
        }

        Debug.Log($"Collected {allSpawnPoints.Count} spawn points.");
    }

    private void Start()
    {
        SpawnEntities();
    }

    void SpawnEntities()
    {
        SpawnByType(SpawnType.HealthPack, numHealthPacks, healthPackPrefab);
        SpawnByType(SpawnType.Astronaut, numAstronauts, astronautPrefab);
        SpawnByType(SpawnType.UFO, numUFOs, ufoPrefab);
    }

    void SpawnByType(SpawnType type, int count, GameObject defaultPrefab)
    {
        List<SpawnPoint> validPoints = allSpawnPoints.FindAll(sp => sp.spawnType == type);

        if (validPoints.Count == 0)
        {
            Debug.LogWarning($"No spawn points for type: {type}");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            SpawnPoint chosen = validPoints[Random.Range(0, validPoints.Count)];

            GameObject prefabToUse = chosen.customPrefab != null ? chosen.customPrefab : defaultPrefab;

            Instantiate(prefabToUse, chosen.transform.position, chosen.transform.rotation);
        }
    }
    private void OnDrawGizmos()
    {
        if (!drawBounds) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, levelSize);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            new Vector3(-levelSize.x / 2, groundHeight, -levelSize.z / 2) + transform.position,
            new Vector3(levelSize.x / 2, groundHeight, levelSize.z / 2) + transform.position
        );
    }
}