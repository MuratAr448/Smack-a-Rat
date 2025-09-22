using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BombRat : Rats
{
    public override void RatHit()
    {
        base.RatHit();
        gameManager.LostLifeTime(2);
        gameManager.lives = 0;
    }
    protected override void RatEffect()
    {
        base.RatEffect();
    }
}
