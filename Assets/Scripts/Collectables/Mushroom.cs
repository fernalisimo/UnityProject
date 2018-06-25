using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{
    protected override void OnRabbitHit(HeroRabbit rabit)
    {
        rabit.makeBigger();
        this.CollectedHide();
    }
}
