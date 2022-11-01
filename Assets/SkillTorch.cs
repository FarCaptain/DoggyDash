using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillTorch : MonoBehaviour
{
    public Skill skilltype;
    public UnityEvent OnGet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var dog = collision.gameObject;
        if (dog.layer != LayerMask.NameToLayer("Player"))
            return;

        switch (skilltype)
        {
            case Skill.CAST:
                dog.transform.GetComponent<StateMachine>().canCast = true;
                break;
            case Skill.POUND:
                dog.transform.GetComponent<StateMachine>().canPound = true;
                break;
            case Skill.EMPTY:
                break;
            default:
                break;
        }
        OnGet?.Invoke();

        this.gameObject.SetActive(false);
    }
}

public enum Skill
{
    CAST,
    POUND,
    EMPTY
}