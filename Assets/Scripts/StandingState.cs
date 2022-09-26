using System.Collections;
using UnityEngine;


public class StandingState : GroundedState
{
    private bool jump;
    private float timePassed;
    private const float waitTime = 5f;

    public StandingState(StateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jump = false;
        timePassed = 0f;

        speed = stateMachine.runSpeed;
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

        timePassed += Time.deltaTime;
        if(timePassed > waitTime)
        {
            animator.SetBool("Sit", true);
        }

        if(horizontalInput != 0f)
        {
            animator.SetBool("Sit", false);
            timePassed = 0f;
        }
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool("Sit", false);
        timePassed = 0f;
    }

}