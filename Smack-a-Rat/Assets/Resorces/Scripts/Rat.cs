using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rat : Rats
{
    public override void RatHit()
    {
        base.RatHit();
        //score +100
    }
    protected override void RatEffect()
    {
        base.RatEffect();
        gameManager.lives -= 1;
        Destroy(gameObject, 0.1f);
    }
}
