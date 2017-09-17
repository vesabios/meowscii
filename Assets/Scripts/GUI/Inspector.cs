using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inspector {

	static int targetIndex = 0;
	static bool pointerOverride = false;

	static List<PWorldObject> visibleObjects;

	public static void CycleTargets() {
		pointerOverride = true;
	}
		
	public static void RefreshObjectList() {

		visibleObjects = World.GetVisibleObjects ();

	}

	public static void Draw() {

		RefreshObjectList ();

		for (int i = 0; i < visibleObjects.Count; i++) {
			PWorldObject worldObject = visibleObjects [i];

			GUI.DrawString (40, i, worldObject.name);


		}

	}





}
