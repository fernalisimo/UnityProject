using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
    public Sprite fruitFull;
    public Sprite crystalFull;

    public string LevelName;

	// Use this for initialization
	void Start () {
        showStatistic();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void showStatistic()
    {
        SpriteRenderer fruit = GameObject.Find("EmptyFruit").GetComponent<SpriteRenderer>();
        SpriteRenderer crystal = GameObject.Find("EmptyCrystal").GetComponent<SpriteRenderer>();
        SpriteRenderer completed = GameObject.Find("Completed").GetComponent<SpriteRenderer>();

        string str = PlayerPrefs.GetString(LevelName + "_stats", null);
        LevelStat stats = JsonUtility.FromJson<LevelStat>(str);
        if (stats == null)
        {
            stats = new LevelStat();
            completed.sprite = null;

        }

        if (stats.hasAllFruits)
        {
            fruit.sprite = fruitFull;
        }

        if (stats.hasCrystals)
        {
            crystal.sprite = crystalFull;
        }

        if (!stats.levelPassed)
        {
            completed.sprite = null;
        }
    }
}
