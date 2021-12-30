using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public void Activate(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Destroy() {
        GameManager.Instance.Score++;

        gameObject.SetActive(false);
        SquareSpawner.Instance.ReturnToPool(this);
    }
}
