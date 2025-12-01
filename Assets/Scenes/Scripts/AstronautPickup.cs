using UnityEngine;

public class AstronautPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public int scoreValue = 50;

    [Header("Pickup Effects")]
    public GameObject pickupEffect;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Collider>().enabled = false;

        if (collected) return;
        if (!other.CompareTag("Player")) return;   // <— ignore everything except the player

        collected = true;

        // Play audio
        AudioManager.Instance.PlayPickup(transform.position);

        // Add score
        ScoreManager.Instance.AddScore(scoreValue);

        // Optional: spawn effect
        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
