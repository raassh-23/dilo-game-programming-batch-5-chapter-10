using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public virtual void Activate(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public virtual void Destroy() {
        gameObject.SetActive(false);
        SquareSpawner.Instance.ReturnToPool(this);
    }
}
