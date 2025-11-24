using UnityEngine;

public class AstronautPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public int scoreValue = 50;

    [Header("Pickup Effects")]
    public GameObject pickupEffect;

    // Prevent multiple triggers
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;  // Ignore additional triggers

        PlayerPawn player = other.GetComponent<PlayerPawn>();

        if (player != null)
        {
            collected = true; // Mark as collected immediately
            AudioManager.Instance.PlayPickup(transform.position);
            // Add score once
            GameManager.Instance.AddScore(scoreValue);

            // Play particle/sfx
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // Destroy the pickup
            Destroy(gameObject);
        }
    }
}