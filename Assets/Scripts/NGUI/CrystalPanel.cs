using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystalColor
{
    Blue = 0,
    Red = 1,
    Green = 2,
}

public class CrystalPanel : MonoBehaviour
{
    public static CrystalPanel current = null;

    public List<UI2DSprite> crystalPlace;

    public Sprite crystalNotGet;
    public List<Sprite> crystalColors;

  public Dictionary<CrystalColor, bool> obrtainedCrystals = new Dictionary<CrystalColor, bool>();

    void Start()
    {
        current = this;
        for(int i = 0; i < 3; i++)
        {
            crystalPlace[i].sprite2D = crystalNotGet;
        }
    }

    public void addCrystal(CrystalColor color)
    {
        obrtainedCrystals[color] = true;
        this.updateCrystalColor(color);
    }

    void updateCrystalColor(CrystalColor color)
    {
        int crystal_id = (int)color;
        if (obrtainedCrystals.ContainsKey(color))
        {
            crystalPlace[crystal_id].sprite2D = crystalColors[crystal_id];
        }
        else
        {
            crystalPlace[crystal_id].sprite2D = crystalNotGet;
        }
    }
}