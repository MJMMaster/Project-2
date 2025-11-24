using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 25f;
    private bool hasHealed = false;   // prevents multiple triggers
    private void OnTriggerEnter(Collider other)
    {
        if (hasHealed) return;  // already triggered once
        AudioManager.Instance.PlayPickup(transform.position);
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

           
            if (playerHealth != null)
            {
                hasHealed = true;   // lock out future triggers
                playerHealth.Heal(healAmount);
                Destroy(gameObject);   // remove health pack
            }
        }
    }
}