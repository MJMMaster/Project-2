using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject explosionPrefab;
    public bool isDead = false;
    public int deathScore;

    [Header("Events")]
    public UnityEvent<float> OnHealthChanged;  // sends percentage 0–1
    public UnityEvent OnDeath;
    private void Awake()
    {
        currentHealth = maxHealth;
        OnHealthChanged.Invoke(currentHealth / maxHealth);
    }

    public void TakeDamage(float amount)
    {
        AudioManager.Instance.PlayDamage(transform.position);

        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged.Invoke(currentHealth / maxHealth);


        if (currentHealth <= 0)
        {
            if (deathScore > 0)
                ScoreManager.Instance.AddScore(deathScore);

            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged.Invoke(currentHealth / maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
    private void Die()
    {
        AudioManager.Instance.PlayDeath(transform.position);

        if (CompareTag("Player"))
        {
            GameManager.Instance.PlayerDied();
        }

        // enemies explode and despawn
        else
        {
            if (explosionPrefab != null)
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        isDead = true;
    }
}