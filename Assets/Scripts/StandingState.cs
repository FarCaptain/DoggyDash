using System.Collections;
using UnityEngine;

// on feet
public class StandingState : MovingState
{
    private bool jump;
    private float timePassed;
    private const float waitTime = 5f;
    private bool cast = false;

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

        // cast spell. Condition: if(can cast - on statemachine I guess
        if(stateMachine.canCast)
            cast = Input.GetKeyDown(KeyCode.J);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(jump)
        {
            stateMachine.SetState(stateMachine.jumpState);
        }

        if(cast)
        {
            animator.SetTrigger("Attack");
            controller.FreezMovement();
            stateMachine.CastFireBall();
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