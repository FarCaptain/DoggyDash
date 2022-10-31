using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoggyBehaviors : MonoBehaviour
{
    public Animator animator;
    public UnityEvent OnDie;

    bool isDead = false;

    public void Die()
    {
        if (isDead)
            return;
        animator.SetTrigger("Die");
        OnDie?.Invoke();
        isDead = true;
    }
}
