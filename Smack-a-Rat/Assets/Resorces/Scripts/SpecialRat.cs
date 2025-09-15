using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SpecialRat : Rats
{
    public override void RatHit()
    {
        base.RatHit();
        //score +300
        Destroy(gameObject, 0.1f);
    }
    protected override void RatEffect()
    {
        base.RatEffect();
        gameManager.lives -= 1;
    }
}
