using UnityEngine;
using System.Collections;

public class ConsoleInputFrame : InputFrame
{

	Vector2 moveVector = Vector2.zero;
	Vector2 cursorMoveVector = Vector2.zero;

	public static ConsoleInputFrame instance;

	protected override void Awake() {
		base.Awake();
		instance = this;
	}
	public static bool IsActive() { return instance.active; }


	void Update()
	{
		if (active)
		{
			if (cursorMoveVector.magnitude > 0)
			{
				//Game.MoveCursor(cursorMoveVector);
			}

			if (!Engine.IsReady()) return;

			if (moveVector.magnitude > 0)
			{
				//Engine.player.TryMoving(moveVector);
				//Engine.ProcessTurn();
			}

		}


	}

	//------------------------------------------------------------------------
	// Standard Input Callbacks
	public override void CheckKeyboard()
	{

		if (Input.GetKeyDown(KeyCode.UpArrow)) { moveVector.y = 1; }
		if (Input.GetKeyDown(KeyCode.DownArrow)) { moveVector.y = -1; }
		if (Input.GetKeyUp(KeyCode.UpArrow)) { moveVector.y = 0; }
		if (Input.GetKeyUp(KeyCode.DownArrow)) { moveVector.y = 0; }
		if (Input.GetKeyDown(KeyCode.LeftArrow)) { moveVector.x = -1; }
		if (Input.GetKeyDown(KeyCode.RightArrow)) { moveVector.x = 1; }
		if (Input.GetKeyUp(KeyCode.LeftArrow)) { moveVector.x = 0; }
		if (Input.GetKeyUp(KeyCode.RightArrow)) { moveVector.x = 0; }


		if (Input.GetKeyDown(KeyCode.F1)) { Painter.Activate(); }
		if (Input.GetKeyDown(KeyCode.F2)) { ObjectEditor.Activate(); }

		if (Input.GetKeyDown(KeyCode.Backspace)) { }
		if (Input.GetKeyDown(KeyCode.Delete)) { }
		if (Input.GetKeyDown(KeyCode.Tab)) { }
		if (Input.GetKeyDown(KeyCode.Return)) { }
		if (Input.GetKeyDown(KeyCode.Pause)) { }
		if (Input.GetKeyDown(KeyCode.Escape)) { }
		if (Input.GetKeyDown(KeyCode.Backspace)) { }
		if (Input.GetKeyDown(KeyCode.Space)) { }
		if (Input.GetKeyDown(KeyCode.Keypad0)) { }
		if (Input.GetKeyDown(KeyCode.Keypad1)) { }
		if (Input.GetKeyDown(KeyCode.Keypad2)) { }
		if (Input.GetKeyDown(KeyCode.Keypad3)) { }
		if (Input.GetKeyDown(KeyCode.Keypad4)) { }
		if (Input.GetKeyDown(KeyCode.Keypad5)) { }
		if (Input.GetKeyDown(KeyCode.Keypad6)) { }
		if (Input.GetKeyDown(KeyCode.Keypad8)) { }
		if (Input.GetKeyDown(KeyCode.Keypad9)) { }
		if (Input.GetKeyDown(KeyCode.KeypadPeriod)) { }
		if (Input.GetKeyDown(KeyCode.KeypadDivide)) { }
		if (Input.GetKeyDown(KeyCode.KeypadMultiply)) { }
		if (Input.GetKeyDown(KeyCode.KeypadMinus)) { }
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) { }
		if (Input.GetKeyDown(KeyCode.KeypadEnter)) { }
		if (Input.GetKeyDown(KeyCode.KeypadEquals)) { }
		if (Input.GetKeyDown(KeyCode.UpArrow)) { }
		if (Input.GetKeyDown(KeyCode.DownArrow)) { }
		if (Input.GetKeyDown(KeyCode.RightArrow)) { }
		if (Input.GetKeyDown(KeyCode.LeftArrow)) { }
		if (Input.GetKeyDown(KeyCode.Insert)) { }
		if (Input.GetKeyDown(KeyCode.Home)) { }
		if (Input.GetKeyDown(KeyCode.End)) { }
		if (Input.GetKeyDown(KeyCode.PageUp)) { }
		if (Input.GetKeyDown(KeyCode.PageDown)) { }
		if (Input.GetKeyDown(KeyCode.F1)) { }
		if (Input.GetKeyDown(KeyCode.F2)) { }
		if (Input.GetKeyDown(KeyCode.F3)) { }
		if (Input.GetKeyDown(KeyCode.F4)) { }
		if (Input.GetKeyDown(KeyCode.F5)) { }
		if (Input.GetKeyDown(KeyCode.F6)) { }
		if (Input.GetKeyDown(KeyCode.F7)) { }
		if (Input.GetKeyDown(KeyCode.F8)) { }
		if (Input.GetKeyDown(KeyCode.F9)) { }
		if (Input.GetKeyDown(KeyCode.F10)) { }
		if (Input.GetKeyDown(KeyCode.F11)) { }
		if (Input.GetKeyDown(KeyCode.F12)) { }
		if (Input.GetKeyDown(KeyCode.Alpha0)) { }
		if (Input.GetKeyDown(KeyCode.Alpha1)) { }
		if (Input.GetKeyDown(KeyCode.Alpha2)) { }
		if (Input.GetKeyDown(KeyCode.Alpha3)) { }
		if (Input.GetKeyDown(KeyCode.Alpha4)) { }
		if (Input.GetKeyDown(KeyCode.Alpha5)) { }
		if (Input.GetKeyDown(KeyCode.Alpha6)) { }
		if (Input.GetKeyDown(KeyCode.Alpha7)) { }
		if (Input.GetKeyDown(KeyCode.Alpha8)) { }
		if (Input.GetKeyDown(KeyCode.Alpha9)) { }
		if (Input.GetKeyDown(KeyCode.Exclaim)) { }
		if (Input.GetKeyDown(KeyCode.DoubleQuote)) { }
		if (Input.GetKeyDown(KeyCode.Hash)) { }
		if (Input.GetKeyDown(KeyCode.Dollar)) { }
		if (Input.GetKeyDown(KeyCode.Ampersand)) { }
		if (Input.GetKeyDown(KeyCode.Quote)) { }
		if (Input.GetKeyDown(KeyCode.LeftParen)) { }
		if (Input.GetKeyDown(KeyCode.RightParen)) { }
		if (Input.GetKeyDown(KeyCode.Asterisk)) { }
		if (Input.GetKeyDown(KeyCode.Plus)) { }
		if (Input.GetKeyDown(KeyCode.Comma)) { }
		if (Input.GetKeyDown(KeyCode.Minus)) { }
		if (Input.GetKeyDown(KeyCode.Period)) { }
		if (Input.GetKeyDown(KeyCode.Slash)) { }
		if (Input.GetKeyDown(KeyCode.Colon)) { }
		if (Input.GetKeyDown(KeyCode.Semicolon)) { }
		if (Input.GetKeyDown(KeyCode.Less)) { }
		if (Input.GetKeyDown(KeyCode.Equals)) { }
		if (Input.GetKeyDown(KeyCode.Greater)) { }
		if (Input.GetKeyDown(KeyCode.Question)) { }
		if (Input.GetKeyDown(KeyCode.At)) { }
		if (Input.GetKeyDown(KeyCode.LeftBracket)) { }
		if (Input.GetKeyDown(KeyCode.Backslash)) { }
		if (Input.GetKeyDown(KeyCode.RightBracket)) { }
		if (Input.GetKeyDown(KeyCode.Caret)) { }
		if (Input.GetKeyDown(KeyCode.Underscore)) { }
		if (Input.GetKeyDown(KeyCode.BackQuote)) { }
		if (Input.GetKeyDown(KeyCode.A)) { }
		if (Input.GetKeyDown(KeyCode.B)) { }
		if (Input.GetKeyDown(KeyCode.C)) { }
		if (Input.GetKeyDown(KeyCode.D)) { }
		if (Input.GetKeyDown(KeyCode.E)) { }
		if (Input.GetKeyDown(KeyCode.F)) { }
		if (Input.GetKeyDown(KeyCode.G)) { }
		if (Input.GetKeyDown(KeyCode.H)) { }
		if (Input.GetKeyDown(KeyCode.I)) { }
		if (Input.GetKeyDown(KeyCode.J)) { }
		if (Input.GetKeyDown(KeyCode.K)) { }
		if (Input.GetKeyDown(KeyCode.L)) { }
		if (Input.GetKeyDown(KeyCode.M)) { }
		if (Input.GetKeyDown(KeyCode.N)) { }
		if (Input.GetKeyDown(KeyCode.O)) { }
		if (Input.GetKeyDown(KeyCode.P)) { }
		if (Input.GetKeyDown(KeyCode.Q)) { }
		if (Input.GetKeyDown(KeyCode.R)) { }
		if (Input.GetKeyDown(KeyCode.S)) { }
		if (Input.GetKeyDown(KeyCode.T)) { }
		if (Input.GetKeyDown(KeyCode.U)) { }
		if (Input.GetKeyDown(KeyCode.V)) { }
		if (Input.GetKeyDown(KeyCode.W)) { }
		if (Input.GetKeyDown(KeyCode.X)) { }
		if (Input.GetKeyDown(KeyCode.Y)) { }
		if (Input.GetKeyDown(KeyCode.Z)) { }


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
