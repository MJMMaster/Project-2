using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPawn : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce;
    public float turnSpeed;
    public float pitchSpeed;
    public float rollSpeed;

    [Header("Weapon Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;   // The place on the model where bullets spawn
    public float projectileSpeed;
    public float fireCooldown;
    private float lastFireTime;


    [Header("References")]
    public Rigidbody rb;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    // Called by Controller (DO NOT read input here)
    public void MoveForward(float amount)
    {
        rb.AddForce(transform.forward * amount * moveForce, ForceMode.Force);
    }

    public void Yaw(float amount)
    {
        rb.AddTorque(Vector3.up * amount * turnSpeed, ForceMode.Force);
    }

    public void Pitch(float amount)
    {
        rb.AddTorque(transform.right * amount * pitchSpeed, ForceMode.Force);
    }

    public void Roll(float amount)
    {
        rb.AddTorque(transform.forward * -amount * rollSpeed, ForceMode.Force);
    }

    public void FireWeapon()
    {
        if (projectilePrefab == null || firePoint == null) return;

        if (Time.time < lastFireTime + fireCooldown)
            return;

        lastFireTime = Time.time;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Add force
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }
    }

}