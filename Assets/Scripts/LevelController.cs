using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    public UILabel coinsLabel;
    public UILabel fruitsLabel;
    public int FruitsNumber;
    public AudioClip music = null;
    public string LevelName;

    AudioSource musicSource = null;

    public LevelStat stat = new LevelStat();


    Vector3 startingPosition;
    int coins = 0;
    int fruits = 0;

    void Awake()
    {
        current = this;
        coins = PlayerPrefs.GetInt("coins", 0);
        fruitsLabel.text = "0/" + FruitsNumber.ToString();
        coinsLabel.text = "0000";
    }

    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = true;
        if (SoundManager.Instance.isMusicOn())
        {
            musicSource.Play();
        }
    }

    void Update()
    {
        if (SoundManager.Instance.isMusicOn() && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
        else if (!SoundManager.Instance.isMusicOn() && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabbitDeath(HeroRabbit rabit)
    {
        rabit.transform.position = this.startingPosition;
    }

	public void addCoins(int number) {
        coins += number;
        string c = coins.ToString();
        string res = "";
        int z = 3 - res.Length;
        for(int i = 0; i < z; i++)
        {
            res += "0";
        }
        coinsLabel.text = res + c;
    }

    public void addFruits(int number, FruitType type)
    {
        fruits += number;
        //stat.collectedFruits[(int)type]++;
        fruitsLabel.text = fruits.ToString() + "/" + FruitsNumber.ToString();
    }

    public bool isAllFruits()
    {
        return FruitsNumber == fruits;
    }

    public int getCoins()
    {
        return coins;
    }

    public int getFruits()
    {
        return fruits;
    }
}
