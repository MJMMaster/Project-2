using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHealthBar : MonoBehaviour
{
    public Image healthFill;
    private Health health;

    void Start()
    {
        health = GetComponentInParent<Health>();
    }

    void Update()
    {
        if (health != null)
        {
            // Convert to float or you'll get only 0 or 1
            float healthPercent = (float)health.currentHealth / health.maxHealth;

            // Update bar fill
            healthFill.fillAmount = healthPercent;

            // Update color from red (low) to green (full)
            healthFill.color = Color.Lerp(Color.red, Color.green, healthPercent);
        }
    }
}