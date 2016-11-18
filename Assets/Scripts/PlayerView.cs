using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour {
    
    private Animator animator;

    private bool isFacingRight;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetVelocity(float velocity)
    {
        animator.SetFloat("Velocity", velocity);
    }

    public void SetVerticalVelocity(float velocity)
    {
        animator.SetFloat("VerticalVelocity", velocity);
    }

    public void SetGrounded(bool grounded)
    {
        animator.SetBool("Grounded", grounded);
    }

    public void TriggerJump()
    {
        animator.SetTrigger("Jump");
    }
}
