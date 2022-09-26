using System.Collections;
using UnityEngine;


public class GroundedState : State
{
    protected float horizontalInput;
    protected float speed;
    private bool onWall;

    public GroundedState(StateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = 1f;
        onWall = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        horizontalInput = Input.GetAxis("Horizontal") * speed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float horizontalMovement = horizontalInput * speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if (onWall && controller.PushingAgainstWall(horizontalInput))
            stateMachine.SetState(stateMachine.wallGrabState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        controller.Move(horizontalInput * speed);

        onWall = controller.m_OnWall;
    }
}