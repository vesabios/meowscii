using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inspector {

	static int targetIndex = 0;
	public static bool pointerOverride = false;
	public static Vector2 pointerPos;

	static List<PWorldObject> visibleObjects;

	public static void CycleTargets() {

		if (!Screen.cursorVisible) {
			Screen.cursorVisible = true;
			pointerOverride = true;
			FixIndex ();

		}
			

		pointerOverride = true;
		IncreaseTargetIndex ();

	}

	public static void IncreaseTargetIndex() {
		if (visibleObjects.Count == 0) {
			targetIndex = -1;
			return;
		}
		Screen.cursorVisible = true;
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


		visibleObjects = World.GetNotableObjects ();

		if (visibleObjects.Contains (Engine.player))
			visibleObjects.Remove (Engine.player);

		if (visibleObjects.Count == 0)
			return;


		FixIndex ();
		System.Guid id = visibleObjects[targetIndex].guid;

		visibleObjects.Sort((x, y) => x.guid.CompareTo(y.guid));
		visibleObjects.Sort((x, y) => x.distFromPlayer.CompareTo(y.distFromPlayer));

		for (int i = 0; i < visibleObjects.Count; i++) {
			if (visibleObjects [i].guid == id) {
				targetIndex = i;
			}
		}

	}

	public static void UpdatePointer() {

	}


	public static void Draw() {



		RefreshObjectList ();

		//GUI.DrawBox (new Rect (1, 1, 20, 1));

		for (int i = 0; i < visibleObjects.Count; i++) {
			PWorldObject worldObject = visibleObjects [i];

			if (targetIndex == i) {

				string s = "";

				pointerPos = Screen.GetWorldPixelInScreenSpace (worldObject.location);

				if (worldObject is PActor) {
					s = ((PActor)worldObject).stateMachine.GetStateStackAsString ()+" ";

					//Debug.Log (s);
					int index = s.LastIndexOf (".");
					if (index > -1) s = s.Substring(index+1);

				}

				s += worldObject.shortDisplayName;

				//Vector2 pos = Screen.GetWorldPixelInScreenSpace (worldObject.location);

				//pos.x -= s.Length / 2;

				//pos.y -= 2;

				GUI.DrawString (1, 1, s, Screen.GenerateBrush(5,2));


			} 






		}


	}

	public static void Dim() {
		pointerOverride = true;
	}





}
