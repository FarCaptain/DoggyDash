using System.Collections;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // current state
    protected State state;

    public StandingState standingState;
    public JumpState jumpState;
    //public WallGrabState wallGrabState;

    [HideInInspector]
    public CharacterController2D controller;
    [HideInInspector]
    public Animator animator;

    [Header("Speeds")]
    public float runSpeed;
    public float airControlSpeed;

    [Header("Ability Locks")]
    public bool canCast = false;
    public bool canPound = false;

    [SerializeField] private GameObject fireballPrefab;
    private Transform socket;

    public bool isGroundPounding = false;

    private void Start()
    {
        if(controller == null)
            controller = GetComponent<CharacterController2D>();
        if (animator == null)
            animator = GetComponent<Animator>();

        standingState = new StandingState(this);
        jumpState = new JumpState(this);
        //wallGrabState = new WallGrabState(this);

        state = standingState;
        state.Enter();

        socket = transform.Find("ProjectSocket");
    }

    public void SetState(State _state)
    {
        Debug.Log("[HERE]ToState" + _state.GetType());

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

    public void CastFireBall()
    {
        var fireball = Instantiate(fireballPrefab);
        fireball.transform.position = socket.transform.position;

        Projectile projectile = fireball.transform.GetComponent<Projectile>();
        float sign = Mathf.Sign(transform.localScale.x);
        projectile.speed *= sign;
        Vector3 scale = fireball.transform.localScale;
        scale.x *= sign;
        fireball.transform.localScale = scale;
    }
}