using System;

[Serializable]

public struct ExpirienceData
{
    public int CurrentLevel;
    public int CurrentXpPoints;

    public ExpirienceData(int level, int xpPoints) {
        CurrentLevel = level;
        CurrentXpPoints = xpPoints;
    }
}
