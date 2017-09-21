using UnityEngine;
using System.Collections;

public class PainterInputFrame : InputFrame
{
	Vector2 cursorMoveVector = Vector2.zero;
	float cursorMoveTimer = 0.0f;
	float cursorRepeatDelay = 0.05f;
	float cursorInitialDelay = 0.5f;
	bool initialDelay = true;

    void Start() { }

	void Update()
	{
		if (active)
		{
			if (cursorMoveVector.magnitude > 0)
			{
				if (cursorMoveTimer == 0.0f) {
					Painter.MoveCursor(cursorMoveVector);
				}

				if (initialDelay) {

					if (cursorMoveTimer > cursorInitialDelay) {
						cursorMoveTimer -= cursorInitialDelay;
						Painter.MoveCursor (cursorMoveVector);
						initialDelay = false;
					}

				} else {
					if (cursorMoveTimer > cursorRepeatDelay) {
						cursorMoveTimer -= cursorRepeatDelay;
						Painter.MoveCursor (cursorMoveVector);
					}

				}

			}

			cursorMoveTimer += Time.deltaTime;
		}
	}

	void ResetCursorTimer() {
		cursorMoveTimer = 0.0f;
		initialDelay = true;
	}



    public override void CheckMouse()
    {

        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
            Painter.IncreaseBrushSize();

        else if (d < 0f)
            Painter.DecreaseBrushSize();

    }

    public override void CheckKeyboard()
    {
		bool leftShift = Input.GetKey (KeyCode.LeftShift);
		bool rightShift = Input.GetKey (KeyCode.RightShift);

		bool shift = leftShift || rightShift;


		if (Input.GetKeyDown (KeyCode.Tab)) {
			Palette.instance.Show ();

		} else if (Input.GetKeyUp (KeyCode.Tab)) {
			Palette.instance.Hide ();
		}


		if (Input.GetKey (KeyCode.LeftAlt)) {


			if (Input.GetKeyDown (KeyCode.F1)) {
				Painter.SetBrushPage (0);
			}
			if (Input.GetKeyDown (KeyCode.F2)) {
				Painter.SetBrushPage (1);
			}
			if (Input.GetKeyDown (KeyCode.F3)) {
				Painter.SetBrushPage (2);
			}
			if (Input.GetKeyDown (KeyCode.F4)) {
				Painter.SetBrushPage (3);
			}
			if (Input.GetKeyDown (KeyCode.F5)) {
				Painter.SetBrushPage (4);
			}
			if (Input.GetKeyDown (KeyCode.F6)) {
				Painter.SetBrushPage (5);
			}
			if (Input.GetKeyDown (KeyCode.F7)) {
				Painter.SetBrushPage (6);
			}
			if (Input.GetKeyDown (KeyCode.F8)) {
				Painter.SetBrushPage (7);
			}
			if (Input.GetKeyDown (KeyCode.F9)) {
				Painter.SetBrushPage (8);
			}
			if (Input.GetKeyDown (KeyCode.F10)) {
				Painter.SetBrushPage (9);
			}

			if (Input.GetKey (KeyCode.H)) {
				Painter.ShowHelp ();
			}

			if (Input.GetKey (KeyCode.UpArrow)) {
				World.ScrollView (new Vector2 (0, -1));
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				World.ScrollView (new Vector2 (0, 1));
			} 
			if (Input.GetKey (KeyCode.RightArrow)) { 
				World.ScrollView (new Vector2 (1, 0));
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				World.ScrollView (new Vector2 (-1, 0));
			}

			if (Input.GetKeyDown (KeyCode.R)) { 
				Painter.ToggleSplatter (); 
			}

			if (Input.GetKeyDown (KeyCode.B)) {
				Painter.SetMode (Painter.Mode.BOX);
			} 

			if (Input.GetKeyDown (KeyCode.M)) {
				Painter.SetMode (Painter.Mode.MOUSE);
			} 

			if (Input.GetKeyDown (KeyCode.K)) {
				Painter.SetMode (Painter.Mode.CURSOR);
			} 

			if (Input.GetKeyDown (KeyCode.C)) {
				Painter.SetMode (Painter.Mode.CAPTURE);
			} 

		} else if (Input.GetKey (KeyCode.LeftControl)) {
			
			if (Input.GetKeyDown (KeyCode.S)) { 
				Painter.SaveImage (); 
			}





		} else {

		



			if (Input.GetKeyDown (KeyCode.F1)) {
				Painter.SetBrushIndex (0);
			}
			if (Input.GetKeyDown (KeyCode.F2)) {
				Painter.SetBrushIndex (1);
			}
			if (Input.GetKeyDown (KeyCode.F3)) {
				Painter.SetBrushIndex (2);
			}
			if (Input.GetKeyDown (KeyCode.F4)) {
				Painter.SetBrushIndex (3);
			}
			if (Input.GetKeyDown (KeyCode.F5)) {
				Painter.SetBrushIndex (4);
			}
			if (Input.GetKeyDown (KeyCode.F6)) {
				Painter.SetBrushIndex (5);
			}
			if (Input.GetKeyDown (KeyCode.F7)) {
				Painter.SetBrushIndex (6);
			}
			if (Input.GetKeyDown (KeyCode.F8)) {
				Painter.SetBrushIndex (7);
			}
			if (Input.GetKeyDown (KeyCode.F9)) {
				Painter.SetBrushIndex (8);
			}
			if (Input.GetKeyDown (KeyCode.F10)) {
				Painter.SetBrushIndex (9);
			}

			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				cursorMoveVector.y = -1;
				ResetCursorTimer ();
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				cursorMoveVector.y = 1;
				ResetCursorTimer ();
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				cursorMoveVector.x = 1;
				ResetCursorTimer ();
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				cursorMoveVector.x = -1;
				ResetCursorTimer ();
			}
			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				cursorMoveVector.y = 0;
			}
			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				cursorMoveVector.y = 0;
			}
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				cursorMoveVector.x = 0;
			}
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				cursorMoveVector.x = 0;
			}

       




			if (Input.GetKeyDown (KeyCode.PageUp)) {
				Painter.IncrementBrushPage ();
			}
			if (Input.GetKeyDown (KeyCode.PageDown)) {
				Painter.DecrementBrushPage ();
			}
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Painter.Deactivate ();
			}
			if (Input.GetKeyDown (KeyCode.Backspace)) {
			}
			if (Input.GetKeyDown (KeyCode.Delete)) {
			}
			if (Input.GetKeyDown (KeyCode.Tab)) {
			}
			if (Input.GetKeyDown (KeyCode.Return)) {
			}
			if (Input.GetKeyDown (KeyCode.Pause)) {
			}
			if (Input.GetKeyDown (KeyCode.Backspace)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad0)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad1)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad2)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad3)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad4)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad5)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad6)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad8)) {
			}
			if (Input.GetKeyDown (KeyCode.Keypad9)) {
			}
			if (Input.GetKeyDown (KeyCode.KeypadPeriod)) {
			}
			if (Input.GetKeyDown (KeyCode.KeypadDivide)) {
			}
			if (Input.GetKeyDown (KeyCode.KeypadMultiply)) {
			}
			if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			}
			if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			}
			if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
			}
			if (Input.GetKeyDown (KeyCode.KeypadEquals)) {
			}

			if (Input.GetKeyDown (KeyCode.Insert)) {
			}
			if (Input.GetKeyDown (KeyCode.Home)) {
			}
			if (Input.GetKeyDown (KeyCode.End)) {
			}

 
			if (Input.GetKeyDown (KeyCode.F4)) {
			}
			if (Input.GetKeyDown (KeyCode.F5)) {
			}
			if (Input.GetKeyDown (KeyCode.F6)) {
			}
			if (Input.GetKeyDown (KeyCode.F7)) {
			}
			if (Input.GetKeyDown (KeyCode.F8)) {
			}
			if (Input.GetKeyDown (KeyCode.F9)) {
			}
			if (Input.GetKeyDown (KeyCode.F10)) {
			}
			if (Input.GetKeyDown (KeyCode.F11)) {
			}
			if (Input.GetKeyDown (KeyCode.F12)) {
			}


			if (Input.GetKeyDown (KeyCode.Exclaim)) {
			}
			if (Input.GetKeyDown (KeyCode.DoubleQuote)) {
			}
			if (Input.GetKeyDown (KeyCode.Hash)) {
			}
			if (Input.GetKeyDown (KeyCode.Dollar)) {
			}
			if (Input.GetKeyDown (KeyCode.Ampersand)) {
			}
			if (Input.GetKeyDown (KeyCode.Quote)) {
			}
			if (Input.GetKeyDown (KeyCode.LeftParen)) {
			}
			if (Input.GetKeyDown (KeyCode.RightParen)) {
			}
			if (Input.GetKeyDown (KeyCode.Asterisk)) {
			}
			if (Input.GetKeyDown (KeyCode.Plus)) {
			}
			if (Input.GetKeyDown (KeyCode.Comma)) {
			}
			if (Input.GetKeyDown (KeyCode.Minus)) {
			}
			if (Input.GetKeyDown (KeyCode.Period)) {
			}
			if (Input.GetKeyDown (KeyCode.Slash)) {
			}
			if (Input.GetKeyDown (KeyCode.Colon)) {
			}
			if (Input.GetKeyDown (KeyCode.Semicolon)) {
			}
			if (Input.GetKeyDown (KeyCode.Less)) {
			}
			if (Input.GetKeyDown (KeyCode.Equals)) {
			}
			if (Input.GetKeyDown (KeyCode.Greater)) {
			}
			if (Input.GetKeyDown (KeyCode.Question)) {
			}
			if (Input.GetKeyDown (KeyCode.At)) {
			}
			if (Input.GetKeyDown (KeyCode.LeftBracket)) {
			}
			if (Input.GetKeyDown (KeyCode.Backslash)) {
			}
			if (Input.GetKeyDown (KeyCode.RightBracket)) {
			}
			if (Input.GetKeyDown (KeyCode.Caret)) {
			}
			if (Input.GetKeyDown (KeyCode.Underscore)) {
			}
			if (Input.GetKeyDown (KeyCode.BackQuote)) {
			}



			if (Input.GetKeyDown (KeyCode.A)) {
				Painter.TextInput (shift ? 'A' : 'a');
			}
			if (Input.GetKeyDown (KeyCode.B)) {
				Painter.TextInput (shift ? 'B' : 'b');
			}
			if (Input.GetKeyDown (KeyCode.C)) {
				Painter.TextInput (shift ? 'C' : 'c');
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				Painter.TextInput (shift ? 'D' : 'd');
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				Painter.TextInput (shift ? 'E' : 'e');
			}
			if (Input.GetKeyDown (KeyCode.F)) {
				Painter.TextInput (shift ? 'F' : 'f');
			}
			if (Input.GetKeyDown (KeyCode.G)) {
				Painter.TextInput (shift ? 'G' : 'g');
			}
			if (Input.GetKeyDown (KeyCode.H)) {
				Painter.TextInput (shift ? 'H' : 'h');
			}
			if (Input.GetKeyDown (KeyCode.I)) {
				Painter.TextInput (shift ? 'I' : 'i');
			}
			if (Input.GetKeyDown (KeyCode.J)) {
				Painter.TextInput (shift ? 'J' : 'j');
			}
			if (Input.GetKeyDown (KeyCode.K)) {
				Painter.TextInput (shift ? 'K' : 'k');
			}
			if (Input.GetKeyDown (KeyCode.L)) {
				Painter.TextInput (shift ? 'L' : 'l');
			}
			if (Input.GetKeyDown (KeyCode.M)) {
				Painter.TextInput (shift ? 'M' : 'm');
			}
			if (Input.GetKeyDown (KeyCode.N)) {
				Painter.TextInput (shift ? 'N' : 'n');
			}
			if (Input.GetKeyDown (KeyCode.O)) {
				Painter.TextInput (shift ? 'O' : 'o');
			}
			if (Input.GetKeyDown (KeyCode.P)) {
				Painter.TextInput (shift ? 'P' : 'p');
			}
			if (Input.GetKeyDown (KeyCode.Q)) {
				Painter.TextInput (shift ? 'Q' : 'q');
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				Painter.TextInput (shift ? 'R' : 'r');
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				Painter.TextInput (shift ? 'S' : 's');
			}
			if (Input.GetKeyDown (KeyCode.T)) {
				Painter.TextInput (shift ? 'T' : 't');
			}
			if (Input.GetKeyDown (KeyCode.U)) {
				Painter.TextInput (shift ? 'U' : 'u');
			}
			if (Input.GetKeyDown (KeyCode.V)) {
				Painter.TextInput (shift ? 'V' : 'v');
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				Painter.TextInput (shift ? 'W' : 'w');
			}
			if (Input.GetKeyDown (KeyCode.X)) {
				Painter.TextInput (shift ? 'X' : 'x');
			}
			if (Input.GetKeyDown (KeyCode.Y)) {
				Painter.TextInput (shift ? 'Y' : 'y');
			}
			if (Input.GetKeyDown (KeyCode.Z)) {
				Painter.TextInput (shift ? 'Z' : 'z');
			}
			if (Input.GetKeyDown (KeyCode.Space)) {
				Painter.TextInput (' ');
			}

		}

    }

 

    public override void OnRightStick(InputState inputState)
    {
    }

    public override void OnLeftStick(InputState inputState)
    {
    }

    public override void OnDpad(InputState inputState)
    {
    }

    public override void OnButtonCrossDown(InputState inputState)
    {
    }

    public override void OnButtonCrossUp(InputState inputState)
    {
    }

    public override void OnButtonCircleDown(InputState inputState)
    {
    }

    public override void OnButtonCircleUp(InputState inputState)
    {
    }

    public override void OnButtonSquareDown(InputState inputState)
    {
    }

    public override void OnButtonSquareUp(InputState inputState)
    {
    }

    public override void OnButtonTriangleDown(InputState inputState)
    {
    }

    public override void OnButtonTriangleUp(InputState inputState)
    {
    }

    public override void OnButtonOptionDown(InputState inputState)
    {
    }

    public override void OnButtonOptionUp(InputState inputState)
    {
    }

    public override void OnLeftStickDown(InputState inputState)
    {
    }

    public override void OnLeftStickUp(InputState inputState)
    {
    }

    public override void OnRightStickDown(InputState inputState)
    {
    }

    public override void OnRightStickUp(InputState inputState)
    {
    }

}
