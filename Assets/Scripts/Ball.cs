using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private float speed = 5;

    private Vector2 direction;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveBall();
    }

    void MoveBall()
    {
        float xMoveDir = Input.GetAxisRaw("Horizontal");
        float yMoveDir = Input.GetAxisRaw("Vertical");
        direction = new Vector2(xMoveDir, yMoveDir);

        rigidbody2d.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "wall")
        {
            direction = Vector2.Reflect(direction, other.contacts[0].normal);
        }
    }
}
