using UnityEngine;
using System.Collections;

[System.Serializable]
public class PWeapon : PEquipment {

    [Header("Weapon"), Space(10)]

    public WeaponType weaponType;
    public WeaponCategory weaponCategory;

    [Space(10), RangeAttribute(1,20)]
    public uint damage;
    [Space(10), RangeAttribute(0,15)]
    public uint modifier;
    [Space(10)]
    public Critical critical;
    public Game.DamageType damageType;

    [Space(10), RangeAttribute(0,150)]
    public uint rangeIncrement = 0;

    [Space(10)]
    public WeaponMagic magic;

    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.MAIN_HAND) return true;
        if (slot == Slot.OFF_HAND)
        {
            if (weaponType == WeaponType.TWO_HANDED) return false;
            if (weaponType == WeaponType.RANGED) return false;
            if (weaponType == WeaponType.THROWN) return false;
            if (weaponType == WeaponType.UNARMED) return false;

            return true;
        }
        return false;
    }

    public enum Critical
    {
        x2,
        x2_DOUBLE,
        x2_TRIPLE,
        x3,
        x3_DOUBLE,
        x3_TRIPLE
    }

    public enum WeaponType
    {
        UNARMED,
        LIGHT,
        ONE_HANDED,
        TWO_HANDED,
        RANGED,
        THROWN
    }

    public enum WeaponCategory
    {
        SIMPLE,
        MARTIAL,
        EXOTIC
    }

    public enum WeaponMagic
    {
        NONE,
        ANARCHIC,
        AXIOMATIC,
        BANE,
        BRILLIANT_ENERGY,
        DANCING,
        DEFENDING,
        DISRUPTION,
        DISTANCE,
        FLAMING,
        FLAMING_BURST,
        FROST,
        GHOST_TOUCH,
        HOLY,
        ICY_BURST,
        KEEN,
        KI_FOCUS,
        MERCIFUL,
        MIGHTY_CLEAVING,
        RETURNING,
        SEEKING,
        SHOCK,
        SHOCKING_BURST,
        SPEED,
        SPELL_STORING,
        THUNDERING,
        THROWING,
        UNHOLY,
        VICIOUS,
        VORPAL,
        WOUNDING
    }

    public override void Draw()
    {
        Color32 brush = Screen.GenerateBrush();
        brush.r = (byte)System.Convert.ToInt32('!');
        Screen.SetWorldPixelInScreenSpace(location, brush, Screen.Layer.FLOATING);

    }


}
