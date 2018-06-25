using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinPopUp : MonoBehaviour {
    public MyButton closeButton;
    public MyButton nextLevel;
    public MyButton repeat;
    public MyButton backgroundButton;

    public List<UI2DSprite> crystalPlace;
    public Sprite crystalNotGet;
    public List<Sprite> crystalColors;

    public AudioClip winingSound = null;
    AudioSource winingSource = null;

    public UILabel coinsLabel;
    public UILabel fruitsLabel;

    // Use this for initialization
    void Start () {
        winingSource = gameObject.AddComponent<AudioSource>();
        winingSource.clip = winingSound;
        if (SoundManager.Instance.isMusicOn())
        {
            winingSource.Play();
        }

        repeat.signalOnClick.AddListener(this.RepeatLevel);
        nextLevel.signalOnClick.AddListener(this.Menu);
        closeButton.signalOnClick.AddListener(this.Menu);
        backgroundButton.signalOnClick.AddListener(this.Menu);

        LevelStat stat = LevelController.current.stat;

        stat.levelPassed = true;
        
        if(CrystalPanel.current.obrtainedCrystals.Count > 3)
        {
            stat.hasCrystals = true;
        }

        for(int i = 0; i < 3; i++)
        {
            int crystal_id = i;
            if (CrystalPanel.current.obrtainedCrystals.ContainsKey((CrystalColor)i))
            {
                crystalPlace[crystal_id].sprite2D = crystalColors[crystal_id];
            }
            else
            {
                crystalPlace[crystal_id].sprite2D = crystalNotGet;
            }
        }

        /*if (stat.collectedFruits[0] > 0 && stat.collectedFruits[1] > 0 && stat.collectedFruits[2] > 0)
        {
            stat.hasAllFruits = true;
        }*/
        stat.hasAllFruits = true;

        fruitsLabel.text = LevelController.current.getFruits() + "/" + LevelController.current.FruitsNumber;

        stat.hasAllFruits = LevelController.current.isAllFruits();

        int coins = LevelController.current.getCoins();
        coinsLabel.text = "+" + coins;
        PlayerPrefs.SetInt("coins", coins);

        // Save
        string str = JsonUtility.ToJson(stat);
        PlayerPrefs.SetString(LevelController.current.LevelName + "_stats", str);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void RepeatLevel()
    {
        SceneManager.LoadScene(LevelDoor.current.levelName);
    }
    
    void Menu()
    {
        SceneManager.LoadScene("ChooseLevelScene");
    }
}
