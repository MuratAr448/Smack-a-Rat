using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SpecialRat : Rats
{
    public override void RatHit()
    {
        base.RatHit();
        gameManager.Score += 300;
    }
    protected override void RatEffect()
    {
        base.RatEffect();
        gameManager.LostLifeTime();
        gameManager.lives -= 1;
    }
}
