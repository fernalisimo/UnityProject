﻿using System.Collections.Generic;

[System.Serializable]
public class LevelStat
{
    public bool hasCrystals = false;
    public bool hasAllFruits = false;
    public bool levelPassed = false;
    public List<int> collectedFruits = new List<int>();
}
