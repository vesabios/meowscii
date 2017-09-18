using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inspector {

	static int targetIndex = 0;
	public static bool pointerOverride = false;
	public static Vector2 pointerPos;

	static List<PWorldObject> visibleObjects;

	public static void CycleTargets() {
		pointerOverride = true;
		IncreaseTargetIndex ();

	}

	public static void IncreaseTargetIndex() {
		if (visibleObjects.Count == 0) {
			targetIndex = -1;
			return;
		}
		targetIndex++;
		FixIndex ();

	}

	public static void DecreaseTargetIndex() {
		if (visibleObjects.Count == 0) {
			targetIndex = -1;
			return;
		}

		targetIndex--;
		FixIndex ();

	}

	static void FixIndex() {
		if (targetIndex >= visibleObjects.Count ) {
			targetIndex = 0;
		} else if (targetIndex < 0 ) {
			targetIndex = visibleObjects.Count-1;
		}
	}


		
	public static void RefreshObjectList() {


		visibleObjects = World.GetVisibleObjects ();

		if (visibleObjects.Count == 0)
			return;

		FixIndex ();
		System.Guid id = visibleObjects[targetIndex].guid;

		visibleObjects.Sort((x, y) => x.guid.CompareTo(y.guid));
		visibleObjects.Sort((x, y) => x.distFromPlayer.CompareTo(y.distFromPlayer));

		for (int i = 0; i < visibleObjects.Count; i++) {
			if (visibleObjects [i].guid == id) {
				//Debug.Log (visibleObjects [i].guid+" : "+id );
				targetIndex = i;
			}
		}

	}

	public static void UpdatePointer() {

	}


	public static void Draw() {

		RefreshObjectList ();

		for (int i = 0; i < visibleObjects.Count; i++) {
			PWorldObject worldObject = visibleObjects [i];

			if (targetIndex == i) {
				GUI.DrawString (2, i, worldObject.shortDisplayName, Screen.GenerateBrush(5,2));

				pointerPos = Screen.GetWorldPixelInScreenSpace (worldObject.location);

			} else {

				GUI.DrawString (2, i, worldObject.shortDisplayName, Screen.GenerateBrush(60,0));
			}


			if (worldObject is PActor) {
				string s = ((PActor)worldObject).stateMachine.GetStateStackAsString ();

				//Debug.Log (s);
				int index = s.LastIndexOf (".");
				if (index > -1) s = s.Substring(index+1);

				GUI.DrawString (10, i, s, Screen.GenerateBrush(62,0));

			}



		}


	}

	public static void Dim() {
		pointerOverride = true;
	}





}
