using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectable
{
    protected override void OnRabbitHit(HeroRabbit rabit)
    {
        HeroRabbit.lastRabit.addHealth(1);
        this.CollectedHide();
    }
}
