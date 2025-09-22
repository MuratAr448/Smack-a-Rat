using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rat : Rats
{
    protected override void Start()
    {
        base.Start();
    }
    public override void RatHit()
    {
        base.RatHit();
        gameManager.Score += 100;
        gameObject.tag = "Untagged";
    }
    protected override void RatEffect()
    {
        base.RatEffect();
        gameManager.LostLifeTime(0);
        gameManager.lives -= 1;
    }
}
