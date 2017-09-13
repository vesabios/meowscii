using UnityEngine;
using System.Collections;

public class PaletteInputFrame : GUIElement
{

	public override void CheckKeyboard()
	{
		if (Input.GetKeyUp (KeyCode.Tab)) {
			Palette.Deactivate ();
		}
	}

	public override void CheckMouse()
	{

		var d = Input.GetAxis("Mouse ScrollWheel");
		if (d > 0f)
			Palette.IncrementCodePage();

		else if (d < 0f)
			Palette.DecrementCodePage();

	}

}
