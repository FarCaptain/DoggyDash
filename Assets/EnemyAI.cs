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

    private bool isMoving = false;

    public float startRange;
    public float attackRange;

    public Transform attackPoint;
    public LayerMask playerLayers;

    private void Update()
    {
        UpdateTimer();

        switch (currentState)
        {
            case CharacterState.IDLE:
                if (!timerStarted)
                {
                    float idleTime = Random.Range(1.5f, 8.5f);
                    StartTimer(idleTime);
                }
                else if (HitTime())
                {
                    SwitchState(CharacterState.MOVING);
                }
                break;
            case CharacterState.MOVING:
                break;
            case CharacterState.Attack:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(transform.position, startRange, playerLayers);
        foreach (var charater in hitCharacters)
        {
            transform.LookAt(charater.transform);
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);
        Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (var charater in hitCharacters)
        {
            Debug.Log("Dead");
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
        Gizmos.DrawWireSphere(transform.position, startRange);
    }
}

public enum CharacterState
{
    IDLE,
    MOVING,
    Attack
}
