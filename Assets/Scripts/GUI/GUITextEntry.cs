using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void TextEntrySubmit(string s);


public class GUITextEntry : GUIText {

	public TextEntrySubmit textEntrySubmit;

	string buffer = "";

	public override void LocalDraw()
	{
		globalRect.width = rect.width = text.Length;
		if (active)
		{
			GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, buffer, activeBrush);

			Screen.SetHardwareCursorPosition (new Vector2 (globalRect.x + cursorIndex, globalRect.y));

		} else
		{
			GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, buffer, brush);

		}


	}

	protected override void Select() {
		if (bCanActivate)
			Activate();
	}

	public override void CheckKeyboard()
	{


		bool leftShift = Input.GetKey (KeyCode.LeftShift);
		bool rightShift = Input.GetKey (KeyCode.RightShift);

		bool shift = leftShift || rightShift;

		if (Input.GetKeyDown(KeyCode.Escape)) { 
			buffer = "";
			ActivateLast();
		}

		if (Input.GetKeyDown(KeyCode.Return)) { 
			textEntrySubmit (buffer);
			buffer = "";
			ActivateLast(); 
		
		}

		if (Input.GetKeyDown(KeyCode.Escape)) { }
		if (Input.GetKeyDown(KeyCode.Backspace)) { 
			if (buffer.Length > 0) {
				buffer = buffer.Substring (0, buffer.Length - 1);

			}
		
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			buffer += (shift ? '!' : '1');
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			buffer += (shift ? '@' : '2');
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			buffer += (shift ? '#' : '3');
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			buffer += (shift ? '$' : '4');
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			buffer += (shift ? '%' : '5');
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			buffer += (shift ? '^' : '6');
		}
		if (Input.GetKeyDown (KeyCode.Alpha7)) {
			buffer += (shift ? '&' : '7');
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			buffer += (shift ? '*' : '8');
		}
		if (Input.GetKeyDown (KeyCode.Alpha9)) {
			buffer += (shift ? '(' : '9');
		}
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			buffer += (shift ? ')' : '0');
		}


		if (Input.GetKeyDown (KeyCode.A)) {
			buffer += (shift ? 'A' : 'a');
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			buffer += (shift ? 'B' : 'b');
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			buffer += (shift ? 'C' : 'c');
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			buffer += (shift ? 'D' : 'd');
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			buffer += (shift ? 'E' : 'e');
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			buffer += (shift ? 'F' : 'f');
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			buffer += (shift ? 'G' : 'g');
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			buffer += (shift ? 'H' : 'h');
		}
		if (Input.GetKeyDown (KeyCode.I)) {
			buffer += (shift ? 'I' : 'i');
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			buffer += (shift ? 'J' : 'j');
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			buffer += (shift ? 'K' : 'k');
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			buffer += (shift ? 'L' : 'l');
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			buffer += (shift ? 'M' : 'm');
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			buffer += (shift ? 'N' : 'n');
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			buffer += (shift ? 'O' : 'o');
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			buffer += (shift ? 'P' : 'p');
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			buffer += (shift ? 'Q' : 'q');
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			buffer += (shift ? 'R' : 'r');
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			buffer += (shift ? 'S' : 's');
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			buffer += (shift ? 'T' : 't');
		}
		if (Input.GetKeyDown (KeyCode.U)) {
			buffer += (shift ? 'U' : 'u');
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			buffer += (shift ? 'V' : 'v');
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			buffer += (shift ? 'W' : 'w');
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			buffer += (shift ? 'X' : 'x');
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			buffer += (shift ? 'Y' : 'y');
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			buffer += (shift ? 'Z' : 'z');
		}


		if (Input.GetKeyDown (KeyCode.Space)) {
			buffer += (' ');
		}

		if (Input.GetKeyDown (KeyCode.Minus)) {
			buffer += (shift ? '_' : '-');
		}
	}

}
