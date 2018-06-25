using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {
    public GameObject settingsPrefab;
    public MyButton pauseButton;

    // Use this for initialization
    void Start () {
		pauseButton.signalOnClick.AddListener(this.ShowSettings);
    }

    void ShowSettings()
    {
        //Знайти батьківський елемент
        GameObject parent = UICamera.first.transform.parent.gameObject;
        //Створити Prefab
        GameObject obj = NGUITools.AddChild(parent, settingsPrefab);
        //Отримати доступ до компоненту (щоб передати параметри)
        SettingsPopUp popup = obj.GetComponent<SettingsPopUp>();
        //...
    }
}
