using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    public float durationDis = 100f;

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
        Destroy(gameObject);
    }
}
