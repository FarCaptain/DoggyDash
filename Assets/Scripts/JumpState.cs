using UnityEngine;

public class JumpState : State
{
    private CharacterController2D controller;
    private bool grounded;

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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(grounded)
            stateMachine.SetState(stateMachine.standingState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        grounded = controller.m_Grounded;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.animator.SetBool("IsJumping", false);
    }
}