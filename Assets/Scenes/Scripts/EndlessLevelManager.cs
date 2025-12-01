
using UnityEngine;
using System.Collections.Generic;

public class EndlessLevelManager : MonoBehaviour
{
    public GameObject levelChunkPrefab;
    public float chunkSize = 100f;            // Size of each chunk
    public int spawnRadius = 1;               // Number of chunks around player to spawn
    public int destroyDistance = 2;           // Distance in chunks to keep before destroying

    private Transform player;

    // Track spawned chunks: coordinate -> chunk GameObject
    private Dictionary<Vector2Int, GameObject> spawnedChunks = new Dictionary<Vector2Int, GameObject>();

    private void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                return;
        }

        Vector2Int playerChunk = new Vector2Int(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.z / chunkSize)
        );

        // Spawn chunks around player
        for (int x = -spawnRadius; x <= spawnRadius; x++)
        {
            for (int z = -spawnRadius; z <= spawnRadius; z++)
            {
                Vector2Int chunkCoord = new Vector2Int(playerChunk.x + x, playerChunk.y + z);
                if (!spawnedChunks.ContainsKey(chunkCoord))
                {
                    SpawnChunk(chunkCoord);
                }
            }
        }

        // Destroy chunks that are too far
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var kvp in spawnedChunks)
        {
            Vector2Int coord = kvp.Key;
            if (Mathf.Abs(coord.x - playerChunk.x) > destroyDistance ||
                Mathf.Abs(coord.y - playerChunk.y) > destroyDistance)
            {
                Destroy(kvp.Value);
                chunksToRemove.Add(coord);
            }
        }

        // Remove destroyed chunks from dictionary
        foreach (var coord in chunksToRemove)
        {
            spawnedChunks.Remove(coord);
        }
    }

    void SpawnChunk(Vector2Int coord)
    {
        Vector3 spawnPos = new Vector3(coord.x * chunkSize, 0, coord.y * chunkSize);
        GameObject newChunk = Instantiate(levelChunkPrefab, spawnPos, Quaternion.identity);
        spawnedChunks.Add(coord, newChunk);
    }
}