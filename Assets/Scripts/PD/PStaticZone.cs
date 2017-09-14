using UnityEngine;
using System.Collections;

public class PStaticZone : PZone {

    public override void LoadAtlas()
    {
        Debug.Log("PStaticZone::LoadAtlas -> " + name);

        Landscape.LoadStaticZone(name);

    }

	public static PStaticZone CreateInstance(string newName)
	{
		var zone = ScriptableObject.CreateInstance<PStaticZone>();
		zone.name = newName;
		return zone;
	}


}
