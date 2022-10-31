using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    private CharacterState currentState = CharacterState.IDLE;

    // Timer
    private float accumulatedTime = 0f;
    private float timeGoal = 0f;
    private bool timerStarted = false;
    private Vector3 originalPos;

    private bool isMoving = false;

    public float startMoveRange;
    public float startAttackRange;
    public float attackRange;

    public Transform attackPoint;
    public LayerMask playerLayers;
    public float speed = 1f;

    private void Awake()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        UpdateTimer();

        Collider2D[] startMoveHits = Physics2D.OverlapCircleAll(transform.position, startMoveRange, playerLayers);
        Collider2D[] startAttackHits = Physics2D.OverlapCircleAll(transform.position, startAttackRange, playerLayers);

        switch (currentState)
        {
            case CharacterState.IDLE:
                //if (!timerStarted)
                //{
                //    float idleTime = Random.Range(1.5f, 8.5f);
                //    StartTimer(idleTime);
                //}
                //else if (HitTime())
                //{
                //    SwitchState(CharacterState.MOVING);
                //}
                animator.SetFloat("Speed", 0f);
                if (startMoveHits.Length != 0)
                {
                    SwitchState(CharacterState.MOVING);
                }

                break;
            case CharacterState.MOVING:
                animator.SetFloat("Speed", speed);

                if(startMoveHits.Length == 0)
                {
                    SwitchState(CharacterState.IDLE);
                }
                else
                {
                    foreach (var charater in startMoveHits)
                    {
                        Vector3 dis = charater.transform.position - transform.position;
                        controller.Move(Mathf.Sign(dis.x) * speed * Time.deltaTime);
                    }
                }

                if(startAttackHits.Length != 0)
                {
                    SwitchState(CharacterState.ATTACK);
                }
                break;
            case CharacterState.ATTACK:
                animator.SetFloat("Speed", 0f);
                Attack();

                if (startAttackHits.Length == 0)
                {
                    animator.SetBool("Attack", false);
                    SwitchState(CharacterState.IDLE);
                }
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);
        Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (var charater in hitCharacters)
        {
            var beh = charater.transform.GetComponent<DoggyBehaviors>();
            beh.Die();

        }
    }

    private void SwitchState(CharacterState state)
    {
        ResetTimer();
        isMoving = false;
        currentState = state;
    }

    #region Timer
    private void ResetTimer()
    {
        accumulatedTime = 0f;
        timeGoal = 0f;
        timerStarted = false;
    }

    private void StartTimer(float _timeGoal)
    {
        timerStarted = true;
        timeGoal = _timeGoal;
    }

    private void UpdateTimer()
    {
        if (timerStarted)
            accumulatedTime += Time.deltaTime;
    }

    private bool HitTime()
    {
        if (timerStarted && accumulatedTime > timeGoal)
            return true;

        return false;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, startMoveRange);
        Gizmos.DrawWireSphere(transform.position, startAttackRange);
    }
}

public enum CharacterState
{
    IDLE,
    MOVING,
    ATTACK
}
