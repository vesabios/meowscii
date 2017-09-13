using UnityEngine;
using System.Collections;

public class ObjectEditorInputFrame : GUIElement
{

    public override void CheckKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { ObjectEditor.Deactivate(); }

		if (Input.GetKey (KeyCode.LeftControl)) {
			
			if (Input.GetKeyDown (KeyCode.S)) {
				World.StoreZoneData ();
			}
		}


    }



}
