using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

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
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged.Invoke(currentHealth / maxHealth);
    }

    private void Die()
    {
        OnDeath.Invoke();

        // If the object has a Death component, let that handle it
        Death deathComponent = GetComponent<Death>();
        if (deathComponent != null)
        {
            deathComponent.Die();
        }
        else
        {
            Destroy(gameObject); // Default behavior
        }
    }
}