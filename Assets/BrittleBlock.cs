using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrittleBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private new BoxCollider2D collider;
    private Animator animator;

    public UnityEvent OnDrop;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    public void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        collider.enabled = false;
        animator.enabled = false;
        //AudioManager.instance.Play("");

        rb.AddForce(new Vector2(0f, -7f));

        OnDrop?.Invoke();
    }

    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.collider.gameObject;
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            animator.SetTrigger("Stepped");
            if(obj.GetComponent<StateMachine>().isGroundPounding)
            {
                Drop();
            }
        }
    }
}
