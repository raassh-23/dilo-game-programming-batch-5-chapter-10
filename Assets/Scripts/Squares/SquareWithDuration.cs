using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareWithDuration : Square
{
    [SerializeField]
    protected float minShowDuration = 3f;

    [SerializeField]
    protected float maxShowDuration = 5f;

    private float showTimer;

    public override void Activate(Vector2 position)
    {
        InitTimer();
        base.Activate(position);
    }

    private void InitTimer()
    {
        showTimer = Random.Range(minShowDuration, maxShowDuration);
    }

    private void Update() {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        showTimer -= Time.deltaTime;

        if (showTimer <= 0f)
        {
            base.Destroy();
        }
    }


}
