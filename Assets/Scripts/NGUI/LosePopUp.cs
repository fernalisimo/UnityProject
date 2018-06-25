using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePopUp : MonoBehaviour {

    public MyButton closeButton;
    public MyButton menuButton;
    public MyButton repeat;
    public MyButton backgroundButton;

    public List<UI2DSprite> crystalPlace;
    public Sprite crystalNotGet;
    public List<Sprite> crystalColors;

    public AudioClip losingSound = null;
    AudioSource losingSource = null;

    // Use this for initialization
    void Start()
    {
        losingSource = gameObject.AddComponent<AudioSource>();
        losingSource.clip = losingSound;
        if (SoundManager.Instance.isMusicOn())
        {
            losingSource.Play();
        }

        closeButton.signalOnClick.AddListener(this.RepeatLevel);
        menuButton.signalOnClick.AddListener(this.Menu);
        closeButton.signalOnClick.AddListener(this.Menu);
        backgroundButton.signalOnClick.AddListener(this.Menu);

        LevelStat stat = LevelController.current.stat;

        stat.levelPassed = true;

        if (CrystalPanel.current.obrtainedCrystals.Count > 3)
        {
            stat.hasCrystals = true;
        }

        for (int i = 0; i < 3; i++)
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
    }

    void RepeatLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    void Menu()
    {
        SceneManager.LoadScene("ChooseLevelScene");
    }
}
