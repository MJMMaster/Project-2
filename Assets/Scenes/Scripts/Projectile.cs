using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 5f; // auto-destroy after time

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Health h = other.GetComponent<Health>();
        if (h != null)
            h.TakeDamage(damage);

        // No physics push!
        Destroy(gameObject);
    }
   
}