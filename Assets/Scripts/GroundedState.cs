using System.Collections;
using UnityEngine;


public class GroundedState : State
{
    protected CharacterController2D controller;
    protected Animator animator;
    protected float horizontalInput;
    private float verticalInput;
    protected float speed;

    public GroundedState(StateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = 1f;

        if (controller == null)
            controller = stateMachine.GetComponent<CharacterController2D>();

        if (animator == null)
            animator = stateMachine.animator;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        horizontalInput = Input.GetAxis("Horizontal") * speed;

        verticalInput = Input.GetAxis("Vertical");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float horizontalMovement = horizontalInput * speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        controller.Move(horizontalInput * speed);
    }
}