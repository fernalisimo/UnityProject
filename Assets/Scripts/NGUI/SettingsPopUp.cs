using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopUp : MonoBehaviour {
    public UI2DSprite soundPlace;
    public UI2DSprite musicPlace;

    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;

    public MyButton closeButton;
    public MyButton backgroundButton;
    public MyButton soundButton;
    public MyButton musicButton;


	// Use this for initialization
	void Start () {
        closeButton.signalOnClick.AddListener(this.CloseSettings);
        backgroundButton.signalOnClick.AddListener(this.CloseSettings);
        soundButton.signalOnClick.AddListener(this.SoundChange);
        musicButton.signalOnClick.AddListener(this.MusicChange);

    }
	
	// Update is called once per frame
	void Update () {
        if (SoundManager.Instance.isSoundOn())
        {
            soundPlace.sprite2D = soundOn;

        } else
        {
            soundPlace.sprite2D = soundOff;
        }

        if (SoundManager.Instance.isMusicOn())
        {
            musicPlace.sprite2D = musicOn;

        }
        else
        {
            musicPlace.sprite2D = musicOff;
        }
    }

    void CloseSettings()
    {
        Destroy(this.gameObject);
    }

    void SoundChange()
    {
        if (SoundManager.Instance.isSoundOn())
        {
            soundPlace.sprite2D = soundOff;
            SoundManager.Instance.setSoundOn(false);
        } else
        {
            soundPlace.sprite2D = soundOn;
            SoundManager.Instance.setSoundOn(true);
        }
    }

    void MusicChange()
    {
        if (SoundManager.Instance.isMusicOn())
        {
            musicPlace.sprite2D = musicOff;
            SoundManager.Instance.setMusicOn(false);
        }
        else
        {
            musicPlace.sprite2D = musicOn;
            SoundManager.Instance.setMusicOn(true);
        }
    }
}
