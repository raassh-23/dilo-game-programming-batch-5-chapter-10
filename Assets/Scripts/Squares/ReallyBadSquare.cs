using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReallyBadSquare : SquareWithDuration
{
    public override void Destroy()
    {
        GameManager.Instance.Timer -= 6;

        base.Destroy();
    }
}
