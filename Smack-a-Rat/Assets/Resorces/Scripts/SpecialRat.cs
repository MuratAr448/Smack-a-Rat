using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SpecialRat : Rats
{
    protected override void Start()
    {
        base.Start();
    }
    public override void RatHit()
    {
        base.RatHit();
        gameManager.Score += 300;
        gameObject.tag = "Untagged";
    }
    protected override void RatEffect()
    {
        base.RatEffect();
        gameManager.LostLifeTime(1);
        gameManager.lives -= 1;
    }
}
