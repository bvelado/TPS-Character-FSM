using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMotor), typeof(PlayerView))]
public class Player : MonoBehaviour {

    private PlayerInput playerInput;
    private PlayerMotor playerMotor;
    private PlayerView playerView;
    private StateMachine playerStateMachine;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMotor = GetComponent<PlayerMotor>();
        playerView = GetComponent<PlayerView>();
    }

    void Start()
    {
        var idleState = new State("Idle", IdleStateEnter, IdleStateExecute, IdleStateExit);
        var walkingState = new State("Walking", WalkingStateEnter, WalkingStateExecute, WalkingStateExit);
        var jumpingState = new State("Jumping", JumpingStateEnter, JumpingStateExecute, JumpingStateExit);

        idleState
            .AddTransition(walkingState, () => Mathf.Abs(playerMotor.Velocity.z) > 0.1f)
            .AddTransition(jumpingState, () => !playerMotor.IsGrounded);

        walkingState
            .AddTransition(idleState, () => Mathf.Abs(playerMotor.Velocity.z) < 0.1f)
            .AddTransition(jumpingState, () => !playerMotor.IsGrounded);

        jumpingState.AddTransition(idleState, () => playerMotor.IsGrounded);


        State[] states = new State[]
        {
            idleState,
            walkingState
        };

        playerStateMachine = new StateMachine(states);
    }

    void Update()
    {
        playerStateMachine.Execute();
    }

    void IdleStateEnter()
    {

    }

    void IdleStateExecute()
    {
        playerMotor.GroundCheck();
        playerMotor.HandleGroundedInputForce(playerInput.GetAxisInput());

        if (playerInput.GetJumpInput())
        {
            playerMotor.HandleJumpInputForce();
            playerMotor.DisableGroundCheck(30);
        }

        playerView.SetVelocity(playerMotor.Velocity.magnitude);
        playerView.SetGrounded(playerMotor.IsGrounded);
    }

    void IdleStateExit()
    {

    }

    void WalkingStateEnter()
    {

    }

    void WalkingStateExecute()
    {
        playerMotor.GroundCheck();
        playerMotor.HandleGroundedInputForce(playerInput.GetAxisInput());

        if (playerInput.GetJumpInput())
        {
            playerMotor.HandleJumpInputForce();
            playerMotor.DisableGroundCheck(30);
        }   

        playerView.SetVelocity(playerMotor.Velocity.magnitude);
        playerView.SetGrounded(playerMotor.IsGrounded);
    }

    void WalkingStateExit()
    {

    }

    void JumpingStateEnter()
    {
        playerView.TriggerJump();
    }

    void JumpingStateExecute()
    {
        playerMotor.GroundCheck();
        playerMotor.HandleAirInputForce(playerInput.GetAxisInput());

        //playerView.SetVelocity(playerMotor.Velocity.magnitude);
        playerView.SetVerticalVelocity(playerMotor.Velocity.y);
        playerView.SetGrounded(playerMotor.IsGrounded);
    }

    void JumpingStateExit()
    {

    }
}
