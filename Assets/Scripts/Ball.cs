using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private float speed = 5;

    private Vector2 direction;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        direction = Random.insideUnitCircle.normalized;
    }

    private void Update() {
        MoveBall();
    }

    void MoveBall()
    {
        rigidbody2d.velocity = direction * speed;
    }
}
