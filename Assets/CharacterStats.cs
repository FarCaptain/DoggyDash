using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHP = 50f;
    public float currentHP;

    public Animator anim;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void Damage(float dam)
    {
        if (currentHP <= 0f)
            return;

            currentHP -= dam;
        if(currentHP <= 0f)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        transform.GetComponent<Rigidbody2D>().gravityScale = 0f;
        transform.GetComponent<Collider2D>().enabled = false;
    }
}
