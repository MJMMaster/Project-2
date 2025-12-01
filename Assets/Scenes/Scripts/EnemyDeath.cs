using UnityEngine;

public class EnemyDeath : Death
{
    public GameObject explosionEffect;
    public int scoreValue = 50;
    public override void Die()
    {
        if (CompareTag("Enemy"))
            ScoreManager.Instance.AddScore(scoreValue);

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);


        Destroy(gameObject);
    }
}