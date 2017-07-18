using UnityEngine;
using System.Collections;

public class Cell
{
    Substances.SubstanceType substanceType;

    public Cell()
    {
        substanceType = Substances.SubstanceType.Stone;
    }

    public Color GetBrush(Vector2 worldLocation, byte lightLevel)
    {
        Substances.BaseSubstance s = Substances.SubstanceProvider.Get(substanceType);
        return s.GetBrush(worldLocation, lightLevel);
    }

}
