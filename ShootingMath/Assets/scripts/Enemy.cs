using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    private Rigidbody rb;
    public int health = 100;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            Vector3 heading = target.position - transform.position;
            Vector3 direction = heading / heading.magnitude;
            direction.z = 0F;
            rb.velocity = direction * speed;
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
