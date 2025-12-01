using UnityEngine;

public class UFOAI : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float damageAmount;

    private Rigidbody rb;
    private Transform player;

    private AudioSource humSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Prevent knockback
        rb.isKinematic = true;

        FindPlayerReference();

        humSource = GetComponent<AudioSource>();
        if (humSource != null)
        {
            humSource.loop = true;
            humSource.Play();
        }
    }

    private void Update()
    {
        // Reacquire player if they died and respawned
        if (player == null)
            FindPlayerReference();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        // Direction toward player
        Vector3 dir = (player.position - transform.position).normalized;

        // Rotate smoothly toward player
        Quaternion targetRot = Quaternion.LookRotation(dir);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotateSpeed * Time.fixedDeltaTime));

        // Move forward
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Health ph = collision.collider.GetComponent<Health>();
            if (ph != null)
                ph.TakeDamage(damageAmount);
        }
    }

    private void FindPlayerReference()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }
}