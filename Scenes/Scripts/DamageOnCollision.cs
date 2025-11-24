using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public float damage = 9999f; // ground = insta-kill

    private void OnCollisionEnter(Collision collision)
    {
        Health h = collision.gameObject.GetComponent<Health>();

        if (h != null)
            h.TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health h = other.GetComponent<Health>();

        if (h != null)
            h.TakeDamage(damage);
    }
}
