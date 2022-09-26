using UnityEngine;

public class WallGrabState : GroundedState
{
    private bool jump;
    private bool onWall;

    public WallGrabState(StateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool("IsGrabbingWall", true);
        jump = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        jump = Input.GetButtonDown("Jump");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (jump)
        {
            stateMachine.SetState(stateMachine.jumpState);
        }
        else if (!controller.PushingAgainstWall(horizontalInput) || !onWall)
        {
            stateMachine.SetState(stateMachine.standingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        onWall = controller.m_OnWall;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.animator.SetBool("IsGrabbingWall", false);
    }
}