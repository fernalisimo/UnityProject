using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void OnRabbitHit(HeroRabbit rabit)
    {
        HeroRabbit.lastRabit.removeHealth(1);
        this.CollectedHide();
    }
}
