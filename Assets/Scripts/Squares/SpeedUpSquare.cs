using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSquare : SquareWithDuration, IPowerUp
{
    void IPowerUp.ApplyEffect()
    {
        GameManager.Instance.Ball.ChangeSpeedByPercent(0.3f);
    }

    public override void Destroy()
    {
        (this as IPowerUp).ApplyEffect();
        GameManager.Instance.SetPowerUpText("Speed ++");

        base.Destroy();
    }
}
