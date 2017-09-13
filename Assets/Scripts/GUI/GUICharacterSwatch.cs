using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CharacterSwatchSelect(int character, int codepage);

public class GUICharacterSwatch : GUIElement {

	public event CharacterSwatchSelect characterSwatchSelect;

	public int character = 60;
	public int codepage = 0;

	public bool selected = false;


	public override void LocalDraw()
	{
		Color32 brush = Screen.GenerateBrush(61, 0, character, codepage);
		GUI.SetPixel((int)globalRect.xMin, (int)globalRect.yMin, brush);
	}

	protected override void Select()
	{
		Debug.Log ("selected swatch: " + character);
		characterSwatchSelect(character, codepage);
	}

	public override void CheckKeyboard()
	{

	}

}
