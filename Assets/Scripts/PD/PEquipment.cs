using UnityEngine;
using System.Collections;

[System.Serializable]
public class PEquipment : PItem {

    [Header("Equipment"), Space(10)]
    [RangeAttribute(0,100)]
    public uint weight;
    public uint cost;
    public bool cursed;

    [HideInInspector]
    public System.Guid actorSoulBinding;

    [Space(20)]
    public StatMod equippedStatMod;

    public enum Slot
    {
        MAIN_HAND,
        OFF_HAND,
        HEAD,
        FACE,
        TORSO,
        BACK,
        ARMS,
        HANDS,
        BODY,
        WAIST,
        FEET,
        LEGS,
        LEFT_RING,
        RIGHT_RING,
        AMULET
    }

    public virtual bool CanEquipInSlot(Slot slot)
    {
        return false;
    }

}


public class PHead : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.HEAD) return true;
        return false;
    }
}

public class PHat : PHead
{

}

public class PMask : PHead
{

}

public class PHelmet : PHead
{

}



public class PAmulet : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.AMULET) return true;
        return false;
    }
}

public class PArms : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.ARMS) return true;
        return false;
    }
}

public class PBracelet : PArms
{
}

public class PBracers : PArms
{
}


public class PCloak : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.BACK) return true;
        return false;
    }
}

public class PBody : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.BODY) return true;
        return false;
    }
}



public class PRobe : PBody
{

}

public class PFace : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.FACE) return true;
        return false;
    }
}

public class PBoots : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.FEET) return true;
        return false;
    }
}

public class PGloves : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.HANDS) return true;
        return false;
    }
}

public class PLeggings : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.LEGS) return true;
        return false;
    }
}

public class PRing : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.LEFT_RING) return true;
        if (slot == Slot.RIGHT_RING) return true;
        return false;
    }
}

public class PShirt : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.TORSO) return true;
        return false;
    }
}

public class PBelt : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.WAIST) return true;
        return false;
    }
}

public class PTool : PEquipment
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.MAIN_HAND) return true;
        return false;
    }
}


public class PShield : PArmor
{
    public override bool CanEquipInSlot(Slot slot)
    {
        if (slot == Slot.OFF_HAND) return true;
        return false;
    }
}