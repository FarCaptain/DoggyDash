using System;
using System.Collections;
using UnityEngine;

public class JumpState : GroundedState
{
    //private CharacterController2D controller;
    private bool grounded;
    private bool doGroundPound;
    private bool isGroundPounding;
    private Rigidbody2D rb;
    private const float dropForce = 25f;

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
        doGroundPound = false;
        isGroundPounding = false;

        speed = stateMachine.airControlSpeed;
        rb = controller.m_Rigidbody2D;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        // pressing down
        if (Input.GetAxis("Vertical") < 0f)
        {
            if(!grounded)
            {
                doGroundPound = true;
            }
        }
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

        if(doGroundPound && !isGroundPounding)
        {
            GroundPoundAttack();
        }

        doGroundPound = false;
    }

    private void GroundPoundAttack()
    {
        isGroundPounding = true;
        controller.enabled = false;
        StopAndAim();
        DropAndSmash();
    }

    private void DropAndSmash()
    {
        //System.Threading.Thread.Sleep(500);
        rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
    }

    private void CompleteGroundPound()
    {
        rb.gravityScale = 1f;
        controller.enabled = true;
        isGroundPounding = false;

        AudioManager.instance.Play("Land");
        controller.ShakeCam();
    }

    private void StopAndAim()
    {
        // clear all forces
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.animator.SetBool("IsJumping", false);

        if(isGroundPounding)
            CompleteGroundPound();
    }
}