using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSquare : Square
{
    public override void Destroy()
    {
        GameManager.Instance.Score++;

        base.Destroy();
    }
}
