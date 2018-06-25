using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Play_Button : MonoBehaviour
{

    public UnityEvent signalOnClick = new UnityEvent();
    public void _onClick()
    {
        this.signalOnClick.Invoke();
    }

    public Play_Button playButton;

    void Start()
    {
        playButton.signalOnClick.AddListener(this.onPlayClick);
    }

    void onPlayClick()
    {
        SceneManager.LoadScene("ChooseLevelScene");
    }
}
