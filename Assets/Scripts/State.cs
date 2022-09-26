using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    protected CharacterController2D controller;
    protected Animator animator;

    public State(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;

        if (controller == null)
            controller = stateMachine.GetComponent<CharacterController2D>();

        if (animator == null)
            animator = stateMachine.animator;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}
