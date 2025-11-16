using UnityEngine;

public class EnemyDeath : Death
{
    public GameObject explosionEffect;
    public int scoreValue = 50;

    public override void Die()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        GameManager.Instance.AddScore(scoreValue);

        Destroy(gameObject);
    }
}