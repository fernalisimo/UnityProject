using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour {

    public static LevelDoor current;
    public string levelName;

    public GameObject winPrefab;

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabbit rabit = collider.GetComponent<HeroRabbit>();
        if (rabit != null)
        {
            current = this;
            GameObject parent = UICamera.first.transform.parent.gameObject;
            GameObject obj = NGUITools.AddChild(parent, winPrefab);
        }
    }
}
