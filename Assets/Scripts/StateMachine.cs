using System.Collections;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // current state
    protected State state;

    public StandingState standingState;
    public JumpState jumpState;

    [HideInInspector]
    public CharacterController2D controller;
    [HideInInspector]
    public Animator animator;

    private void Start()
    {
        if(controller == null)
            controller = GetComponent<CharacterController2D>();
        if (animator == null)
            animator = GetComponent<Animator>();

        standingState = new StandingState(this);
        jumpState = new JumpState(this);

        state = standingState;
        state.Enter();
    }

    public void SetState(State _state)
    {
        state.Exit();
        state = _state;
        state.Enter();
    }

    private void Update()
    {
        state.HandleInput();
        state.LogicUpdate();
    }

    private void FixedUpdate()
    {
        state.PhysicsUpdate();
    }
}