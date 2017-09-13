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

	public override void Draw()
	{
		Color32 brush = Screen.GenerateBrush();
		brush.r = (byte)System.Convert.ToInt32('/');
		Screen.SetWorldPixelInScreenSpace(location, brush, Screen.Layer.FLOATING);

	}
}
