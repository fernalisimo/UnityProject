using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public string LevelName;


    void OnTriggerEnter2D(Collider2D collider)
    {
        SceneManager.LoadScene(LevelName);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        OnTriggerEnter2D(collider);
    }
}
