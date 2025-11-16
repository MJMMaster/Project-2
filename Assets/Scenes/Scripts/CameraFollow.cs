using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target (Auto-Assigned)")]
    public Transform target;  // Auto set to current player

    [Header("Offset Settings")]
    public Vector3 offsetDirection = new Vector3(0, 3f, -6f);
    public float offsetMagnitude;
    public float minOffset;
    public float maxOffset;

    [Header("Look At Settings")]
    public Vector3 lookAtOffset = new Vector3(0, 1f, 5f);

    [Header("Smoothing")]
    public float followSmooth;
    public float rotateSmooth;

    void Start()
    {
        // Grab the player once the scene starts
        TryAssignPlayer();
    }

    void Update()
    {
        // If something respawned, reassign
        if (!target)
            TryAssignPlayer();

        if (!target)
            return;

        HandleOffsetInput();
    }

    void LateUpdate()
    {
        if (!target) return;

        // Compute actual offset
        Vector3 desiredOffset = target.TransformDirection(offsetDirection.normalized * offsetMagnitude);

        // Smoothly follow the ship
        Vector3 desiredPos = Vector3.Lerp(transform.position,
                                          target.position + desiredOffset,
                                          followSmooth * Time.deltaTime);

        transform.position = desiredPos;

        // Look toward a point in front of the ship
        Vector3 lookTarget = target.TransformPoint(lookAtOffset);

        Quaternion desiredRot = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotateSmooth * Time.deltaTime);
    }

    // Automatically find the player's ship transform
    void TryAssignPlayer()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.currentPlayer != null)
            target = GameManager.Instance.currentPlayer.transform;
    }

    void HandleOffsetInput()
    {
        // Increase offset with O
        if (Input.GetKey(KeyCode.O))
            offsetMagnitude += Time.deltaTime;

        // Decrease offset with L
        if (Input.GetKey(KeyCode.L))
            offsetMagnitude -= Time.deltaTime;

        // Clamp values
        offsetMagnitude = Mathf.Clamp(offsetMagnitude, minOffset, maxOffset);
    }
}