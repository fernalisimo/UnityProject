using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Ork_Battle_Contoller : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.gameObject.layer == 13 && Green_Ork_Hero.current != null
            && !Green_Ork_Hero.current.isDead() && !HeroRabbit.lastRabit.isDead())
        {
            Green_Ork_Hero.current.showAttack();
            HeroRabbit.lastRabit.removeHealth(1);
        }
        else if (this.gameObject.layer == 14 && HeroRabbit.lastRabit != null
            && !HeroRabbit.lastRabit.isDead() && Green_Ork_Hero.current != null)
        {
            Green_Ork_Hero.current.removeHealth(1);
            Debug.Log("remove health");
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        OnTriggerEnter2D(collider);
    }
}
