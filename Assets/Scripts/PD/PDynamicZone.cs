using UnityEngine;
using System.Collections;

public class PDynamicZone : PZone {


	public static PDynamicZone CreateInstance(string newName)
	{
		var zone = ScriptableObject.CreateInstance<PDynamicZone>();
		zone.name = newName;
		return zone;
	}

}
