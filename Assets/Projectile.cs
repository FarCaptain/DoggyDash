using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    public float durationDis = 100f;
    public float damage = 25f;

    private Vector3 initPos;

    private void Awake()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

        if (Vector3.Distance(initPos, transform.position) > durationDis)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var colliderObj = collision.collider.gameObject;
        int wat = LayerMask.GetMask("Enemy");
        if (colliderObj.layer == LayerMask.NameToLayer("Enemy"))
        {
            var stats = colliderObj.GetComponent<CharacterStats>();
            if(stats != null)
            {
                stats.Damage(damage);
            }
        }
        Destroy(gameObject);
    }
}
