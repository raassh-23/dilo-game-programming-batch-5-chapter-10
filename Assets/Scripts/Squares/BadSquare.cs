using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSquare : SquareWithDuration
{
    public override void Destroy()
    {
        GameManager.Instance.Score -= 3;

        base.Destroy();
    }
}
