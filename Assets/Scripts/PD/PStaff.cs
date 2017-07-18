using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StaffSpell
{
    public SpellName spell;
    public uint charges;
}

public class PStaff : PWeapon {

    public List<StaffSpell> spells;

    public PStaff() {
        weight = 4;
        damage = 3;
        weaponType = WeaponType.TWO_HANDED;
        damageType = Game.DamageType.BLUDGEONING;
    }

    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.MAIN_HAND) return true;
        return false;
    }
}
