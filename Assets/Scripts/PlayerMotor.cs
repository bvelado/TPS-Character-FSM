using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
    
    private Rigidbody playerRigidbody;
    private CapsuleCollider playerCollider;
    private BoxCollider feetCollider;

    [SerializeField]
    private float groundedDrag = 10f;
    [SerializeField]
    private float airDrag = 0.6f;

    public Vector3 Velocity { get { return playerRigidbody.velocity; } }
    
    public float GroundedAcceleration;
    public float MaxGroundedSpeed;

    public float AirAcceleration;
    public float MaxAirSpeed;

    public LayerMask GroundLayer;
    public float GroundCheckDistance;
    private bool isGrounded;
    public bool IsGrounded { get { return isGrounded; } }

    private int framesOfDisabledGroundCheck;

    public float JumpForce;

    void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        feetCollider = GetComponent<BoxCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    public void HandleGroundedInputForce(Vector3 inputForce)
    {
        playerRigidbody.drag = groundedDrag;

        if (Mathf.Abs(inputForce.magnitude) > 0.1f)
            transform.LookAt(transform.position + inputForce);

        Vector3 vel = new Vector3();

        if (isGrounded)
        {
            vel.x = Mathf.Clamp(playerRigidbody.velocity.x + (inputForce.normalized.x * GroundedAcceleration), -MaxGroundedSpeed, MaxGroundedSpeed);
            vel.y = playerRigidbody.velocity.y;
            vel.z = Mathf.Clamp(playerRigidbody.velocity.z + (inputForce.normalized.z * GroundedAcceleration), -MaxGroundedSpeed, MaxGroundedSpeed);
            playerRigidbody.velocity = vel;
        }
    }

    public void HandleAirInputForce(Vector3 inputForce)
    {
        playerRigidbody.drag = airDrag;

        if (Mathf.Abs(inputForce.magnitude) > 0.1f)
            transform.LookAt(transform.position + inputForce);

        Vector3 vel = new Vector3();

        if (!isGrounded)
        {
            vel.x = Mathf.Clamp(playerRigidbody.velocity.x + (inputForce.normalized.x * AirAcceleration), -MaxAirSpeed, MaxAirSpeed);
            vel.y = playerRigidbody.velocity.y;
            vel.z = Mathf.Clamp(playerRigidbody.velocity.z + (inputForce.normalized.z * AirAcceleration), -MaxAirSpeed, MaxAirSpeed);
            playerRigidbody.velocity = vel;
        }
    }

    public void HandleJumpInputForce()
    {
        playerRigidbody.drag = airDrag;
        Vector3 vel = new Vector3(playerRigidbody.velocity.x, JumpForce, playerRigidbody.velocity.z);

        playerRigidbody.velocity = vel;
    }

    public void GroundCheck()
    {
        //if (framesOfDisabledGroundCheck > 0)
        //    framesOfDisabledGroundCheck--;
        //else
        //    isGrounded = Physics.Raycast(transform.position + (Vector3.up * (GroundCheckDistance/2f)), Vector3.down, GroundCheckDistance, GroundLayer.value);

        isGrounded = false;
        isGrounded |= Physics.Raycast(transform.position + (transform.forward * feetCollider.size.z / 2f) + (transform.right * feetCollider.size.x / 2f) + (Vector3.up * (GroundCheckDistance / 2f)), Vector3.down, GroundCheckDistance, GroundLayer.value);
        isGrounded |= Physics.Raycast(transform.position - (transform.forward * feetCollider.size.z / 2f) + (transform.right * feetCollider.size.x / 2f) + (Vector3.up * (GroundCheckDistance / 2f)), Vector3.down, GroundCheckDistance, GroundLayer.value);
        isGrounded |= Physics.Raycast(transform.position + (transform.forward * feetCollider.size.z / 2f) - (transform.right * feetCollider.size.x / 2f) + (Vector3.up * (GroundCheckDistance / 2f)), Vector3.down, GroundCheckDistance, GroundLayer.value);
        isGrounded |= Physics.Raycast(transform.position - (transform.forward * feetCollider.size.z / 2f) - (transform.right * feetCollider.size.x / 2f) + (Vector3.up * (GroundCheckDistance / 2f)), Vector3.down, GroundCheckDistance, GroundLayer.value);
    }

    public void DisableGroundCheck(int numberOfFrames)
    {
        isGrounded = false;
        framesOfDisabledGroundCheck = numberOfFrames;
    }
}
