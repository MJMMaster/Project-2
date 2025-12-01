using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerPawn pawn;

    private void Awake()
    {
        pawn = GetComponent<PlayerPawn>();
        if (pawn == null)
            Debug.LogError("PlayerController requires PlayerPawn on the same GameObject.");
    }

    private void Update()
    {
        HandleMovementInput();

        {
            if (pawn == null) return;

            // Fire on left mouse button or space
            if (Input.GetButtonDown("Fire1"))
            {
                pawn.FireWeapon();
            }
            // Fire on left mouse button or space
            if (Input.GetButtonDown("Fire2"))
            {
                pawn.FireBigWeapon();
            }
        }
    }
    private void HandleMovementInput()
    {
        // W/S  Forward / Backward
        if (Input.GetKey(KeyCode.W))
            pawn.MoveForward(1f);
        if (Input.GetKey(KeyCode.S))
            pawn.MoveForward(-1f);

        // A/D  Yaw (turn left/right)
        if (Input.GetKey(KeyCode.A))
            pawn.Yaw(-1f);
        if (Input.GetKey(KeyCode.D))
            pawn.Yaw(1f);

        // Q/E  Roll
        if (Input.GetKey(KeyCode.Q))
            pawn.Roll(1f);
        if (Input.GetKey(KeyCode.E))
            pawn.Roll(-1f);

        // Z/X  Pitch
        if (Input.GetKey(KeyCode.Z))
            pawn.Pitch(1f);
        if (Input.GetKey(KeyCode.X))
            pawn.Pitch(-1f);
    }
}