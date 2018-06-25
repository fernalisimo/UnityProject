using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPanel : MonoBehaviour
{
    public static LivesPanel current;

    public List<UI2DSprite> hearts;

    public Sprite striteLiveFull;
    public Sprite striteLiveEmpty;

    void Start()
    {
        current = this;
    }

    public void setLivesQuantity(int lives)
    {
        for(int i = 0; i < 3; ++i) {
            if (i < lives)
            {
                hearts[i].sprite2D = this.striteLiveFull;
            }
            else
            {
                hearts[i].sprite2D = this.striteLiveEmpty;
            }
        }
    }
}