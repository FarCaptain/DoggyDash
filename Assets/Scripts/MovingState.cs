using System.Collections;
using UnityEngine;


public class MovingState : State
{
    protected float horizontalInput;
    protected float speed;
    private bool onWall;

    public MovingState(StateMachine _stateMachine) : base(_stateMachine)
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
        horizontalInput = Input.GetAxis("Horizontal");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float horizontalMovement = horizontalInput * speed;
        if (!controller.m_EnableMovement)
            horizontalMovement = 0f;

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