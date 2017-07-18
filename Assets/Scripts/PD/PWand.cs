using UnityEngine;
using System.Collections;

[System.Serializable]
public class PWand : PEquipment {

    // spell

    public uint charges;
    public uint cooldownTime;
    [HideInInspector]
    public uint cooldown;

    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.MAIN_HAND) return true;
        if (slot == Slot.OFF_HAND) return true;
        return false;
    }
}
