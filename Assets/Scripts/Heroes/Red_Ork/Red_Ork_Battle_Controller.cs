using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2BattleController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
            Red_Ork_Hero.current.removeHealth(1);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        OnTriggerEnter2D(collider);
    }
}