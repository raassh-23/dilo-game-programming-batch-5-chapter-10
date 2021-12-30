using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSquare : Square
{
    public override void Destroy()
    {
        GameManager.Instance.Score += 3;
        GameManager.Instance.Timer += 5;

        base.Destroy();
    }
}
