using System.Collections;
using UnityEngine;


public class StandingState : GroundedState
{
    private bool jump;

    public StandingState(StateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
        if(jump)
        {
            stateMachine.SetState(stateMachine.jumpState);
        }
    }

}