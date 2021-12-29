using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Text ScoreText;

    private Vector2 direction;

    private int _score = 0;

    public int Score
    {
        get
        {
            return _score;
        }

        private set
        {
            _score = value;
            ScoreText.text = "Score: " + _score;
        }
    }

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveBall();
    }

    void MoveBall()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 curPos = transform.position;

        if (Vector2.Distance(mousePos, curPos) > 0.1f)
        {
            direction = (mousePos - curPos).normalized;
        }
        else
        {
            direction = Vector2.zero;
        }

        rigidbody2d.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "square")
        {
            Score++;
            SquareSpawner.Instance.DestroySquare(other.gameObject);
        }
    }
}
