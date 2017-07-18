using UnityEngine;
using System.Collections;

[System.Serializable]
public class PArmor : PBody
{
    public enum ArmorType
    {
        LIGHT,
        MEDIUM,
        HEAVY
    }

    public uint armorBonus;
    public int maxDexBonus;
    public int armorCheckPenalty;
    [RangeAttribute(0.0f, 1.0f)]
    public float arcaneSpellFailureChance;
    public ArmorType armorType;
}