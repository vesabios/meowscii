
using UnityEngine;
using System;
using System.Collections;

public class GamePad : InputFrame
{
	// Custom class for holding all the gamepad sprites

	[System.Serializable]
	public class PS4GamePad
	{
		public SpriteRenderer thumbstick_left;
		public SpriteRenderer thumbstick_right;

		public SpriteRenderer cross;
		public SpriteRenderer circle;
		public SpriteRenderer triangle;
		public SpriteRenderer square;

		public SpriteRenderer dpad_down;
		public SpriteRenderer dpad_right;
		public SpriteRenderer dpad_up;
		public SpriteRenderer dpad_left;

		public SpriteRenderer L1;
		public SpriteRenderer L2;
		public SpriteRenderer R1;
		public SpriteRenderer R2;

		public SpriteRenderer lightbar;
		public SpriteRenderer options;
		public SpriteRenderer speaker;
		public SpriteRenderer touchpad;
		public Transform gyro;
		public TextMesh text;
		public Light light;
	}

	public PS4GamePad gamePad;

	public Color inputOn = Color.white;
	public Color inputOff = Color.grey;


	void Start()
	{
		Activate();

	}


	public override void OnRightStick(InputState inputState) {
		gamePad.thumbstick_right.transform.localPosition = new Vector3(0.4f*inputState.rightStick.x,
		                                                               0.4f*inputState.rightStick.y,
		                                                               0);

	}

	public override void OnLeftStick(InputState inputState) {
		gamePad.thumbstick_left.transform.localPosition = new Vector3(0.4f*inputState.leftStick.x,
		                                                              0.4f*inputState.leftStick.y,
		                                                              0);
	}

	public override void OnDpad(InputState inputState) {
		if(inputState.dpad.x > 0)
			gamePad.dpad_right.color = inputOn;
		else
			gamePad.dpad_right.color = inputOff;

		if(inputState.dpad.x < 0)
			gamePad.dpad_left.color = inputOn;
		else
			gamePad.dpad_left.color = inputOff;

		if(inputState.dpad.y > 0)
			gamePad.dpad_up.color = inputOn;
		else
			gamePad.dpad_up.color = inputOff;

		if(inputState.dpad.y < 0)
			gamePad.dpad_down.color = inputOn;
		else
			gamePad.dpad_down.color = inputOff;
	}

	public override void OnButtonCrossDown(InputState inputState) {
		gamePad.cross.color = inputOn;
	}

	public override void OnButtonCrossUp(InputState inputState) {
		gamePad.cross.color = inputOff;
	}

	public override void OnButtonCircleDown(InputState inputState) {
		gamePad.circle.color = inputOn;
	}

	public override void OnButtonCircleUp(InputState inputState) {
		gamePad.circle.color = inputOff;
	}

	public override void OnButtonSquareDown(InputState inputState) {
		gamePad.square.color = inputOn;
	}

	public override void OnButtonSquareUp(InputState inputState) {
		gamePad.square.color = inputOff;
	}

	public override void OnButtonTriangleDown(InputState inputState) {
		gamePad.triangle.color = inputOn;
	}

	public override void OnButtonTriangleUp(InputState inputState) {
		gamePad.triangle.color = inputOff;
	}	

	public override void OnButtonOptionDown(InputState inputState) {
		gamePad.options.color = inputOn;
	}

	public override void OnButtonOptionUp(InputState inputState) {
		gamePad.options.color = inputOff;
	}	

	public override void OnLeftStickDown(InputState inputState) {
		gamePad.thumbstick_left.color = inputOn;
	}

	public override void OnLeftStickUp(InputState inputState) {
		gamePad.thumbstick_left.color = inputOff;
	}

	public override void OnRightStickDown(InputState inputState) {
		gamePad.thumbstick_right.color = inputOn;
	}

	public override void OnRightStickUp(InputState inputState) {
		gamePad.thumbstick_right.color = inputOff;
	}

	public override void OnR1(InputState inputState) {

		if (inputState.R1) {
			gamePad.R1.color = inputOn;
		} else {
			gamePad.R1.color = inputOff;
		}

	}

	public override void OnR2(InputState inputState) {

		if (inputState.R2>0) {
			gamePad.R2.color = inputOff+(inputOn*inputState.R2);
		} else {
			gamePad.R2.color = inputOff;
		}

	}

	public override void OnL1(InputState inputState) {

		if (inputState.L1) {
			gamePad.L1.color = inputOn;
		} else {
			gamePad.L1.color = inputOff;
		}

	}

	public override void OnL2(InputState inputState) {

		if (inputState.L2>0) {
			gamePad.L2.color = inputOff+(inputOn*inputState.L2);
		} else {
			gamePad.L2.color = inputOff;
		}

	}


}

