using UnityEngine;

public class JumpState : GroundedState
{
    //private CharacterController2D controller;
    private bool grounded;
    private bool onWall;

    public JumpState(StateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (controller == null)
            controller = stateMachine.controller;

        // If allows air control, Jump State has to be "grounded" as well...
        controller.Jump();
        stateMachine.animator.SetBool("IsJumping", true);
        grounded = false;
        onWall = false;

        speed = stateMachine.airControlSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(grounded)
            stateMachine.SetState(stateMachine.standingState);
        if (onWall && controller.PushingAgainstWall(horizontalInput))
            stateMachine.SetState(stateMachine.wallGrabState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        grounded = controller.m_Grounded;
        onWall = controller.m_OnWall;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.animator.SetBool("IsJumping", false);
    }
}