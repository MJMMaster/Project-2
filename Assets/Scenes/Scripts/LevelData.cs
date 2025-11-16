using UnityEngine;

public class LevelData : MonoBehaviour
{
    [Header("Level Dimensions")]
    [Tooltip("Size of the playable area in meters.")]
    public Vector3 levelSize = new Vector3(100f, 100f, 100f);

    [Header("Ground Settings")]
    [Tooltip("Y position where the ground plane exists.")]
    public float groundHeight = 0f;

    [Header("Spawn Settings")]
    [Tooltip("Default spawn point for the player.")]
    public Transform playerSpawnPoint;

    [Tooltip("Optional enemy spawn points or future use points.")]
    public Transform[] spawnPoints;

    [Header("Debug")]
    public bool drawBounds = true;

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