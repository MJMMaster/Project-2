using UnityEngine;

public enum SpawnType
{
    None,
    HealthPack,
    Astronaut,
    UFO
}

public class SpawnPoint : MonoBehaviour
{
    public SpawnType spawnType = SpawnType.None;

    [Tooltip("Override prefab (optional). If null, LevelData will use its default prefab for this type.")]
    public GameObject customPrefab;

    [Tooltip("If disabled, LevelData will ignore this spawn point.")]
    public bool active = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}