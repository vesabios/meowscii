
using UnityEngine;
using System;
using System.Collections;



public class Keyboard : MonoBehaviour
{

	#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

	public InputState inputState = new InputState();

	void Start()
	{
	
	}

	void Update()
	{

		Thumbsticks();
		InputButtons();
		DPadButtons();
		TriggerShoulderButtons();

	}

	/*
	button 9 = right stick push in
	button 8 = left stick push in
	button 7 = start button
	*/

	void Thumbsticks()
	{

/*
		Vector2 leftStick = new Vector2(
			Input.GetAxis("logitech_leftstick" + stickID + "horizontal"),
			Input.GetAxis("logitech_leftstick" + stickID + "vertical")
		);

		Vector2 rightStick = new Vector2(
			Input.GetAxis("logitech_rightstick" + stickID + "horizontal"),
			Input.GetAxis("logitech_rightstick" + stickID + "vertical")
		);

		if (inputState.leftStick != leftStick) {
			inputState.leftStick = leftStick;
			Messenger.Broadcast("leftStick", inputState);
		}

		if (inputState.rightStick != rightStick) {
			inputState.rightStick = rightStick;
			Messenger.Broadcast("rightStick", inputState);
		}


		if(Input.GetButton("logitech_leftstick0button")) {
			if (!inputState.leftStickButton) {
				inputState.leftStickButton = true;
				Messenger.Broadcast("leftStickDown", inputState);
			}			
		} else {
			if (inputState.leftStickButton) {
				inputState.leftStickButton = false;
				Messenger.Broadcast("leftStickUp", inputState);
			}	
		}

		if(Input.GetButton("logitech_rightstick0button")) {
			if (!inputState.rightStickButton) {
				inputState.rightStickButton = true;
				Messenger.Broadcast("rightStickDown", inputState);
			}			
		} else {
			if (inputState.rightStickButton) {
				inputState.rightStickButton = false;
				Messenger.Broadcast("rightStickUp", inputState);
			}	
		}		
		*/

	}

	void InputButtons()
	{
/*
		if(Input.GetButton("logitech_buttonA")) {
			if (!inputState.cross) {
				inputState.cross = true;
				Messenger.Broadcast("buttonCrossDown", inputState);
			}
		} else {
			if (inputState.cross) {
				inputState.cross = false;
				Messenger.Broadcast("buttonCrossUp", inputState);
			}
		}

		if(Input.GetButton("logitech_buttonB")) {
			if (!inputState.circle) {
				inputState.circle = true;
				Messenger.Broadcast("buttonCircleDown", inputState);
			}
		} else {
			if (inputState.circle) {
				inputState.circle = false;
				Messenger.Broadcast("buttonCircleUp", inputState);
			}
		}

		if(Input.GetButton("logitech_buttonX")) {
			if (!inputState.square) {
				inputState.square = true;
				Messenger.Broadcast("buttonSquareDown", inputState);
			}
		} else {
			if (inputState.square) {
				inputState.square = false;
				Messenger.Broadcast("buttonSquareUp", inputState);
			}
		}

		if(Input.GetButton("logitech_buttonY")) {
			if (!inputState.triangle) {
				inputState.triangle = true;
				Messenger.Broadcast("buttonTriangleDown", inputState);
			}			
		} else {
			if (inputState.triangle) {
				inputState.triangle = false;
				Messenger.Broadcast("buttonTriangleUp", inputState);
			}	
		}

		if(Input.GetButton("logitech_start")) {
			if (!inputState.option) {
				inputState.option = true;
				Messenger.Broadcast("buttonOptionDown", inputState);
			}			
		} else {
			if (inputState.option) {
				inputState.option = false;
				Messenger.Broadcast("buttonOptionUp", inputState);
			}	
		}
*/


		if (Input.GetKey(KeyCode.Space) && !inputState.cross) {
			inputState.cross = true;
			Messenger.Broadcast("buttonCrossDown", inputState);			
		} else if (inputState.cross) {
			inputState.cross = false;
			Messenger.Broadcast("buttonCrossUp", inputState);			
		}



	}

	void DPadButtons()
	{




		Vector2 dpad = Vector2.zero;

/*
		if(Input.GetAxis("logitech_dpad_horizontal") > 0) {
			dpad.x = 1.0f;
		} else if(Input.GetAxis("logitech_dpad_horizontal") < 0) {
			dpad.x = -1.0f;
		} 

		if(Input.GetAxis("logitech_dpad_vertical") < 0) {
			dpad.y = -1.0f;
		} else if(Input.GetAxis("logitech_dpad_vertical") > 0) {
			dpad.y = 1.0f;
		}

*/	
	
  		if (Input.GetKey(KeyCode.UpArrow)) {
  			dpad.y = 1.0f;
  		} else if (Input.GetKey(KeyCode.DownArrow)) {
  			dpad.y = -1.0f;
  		}

  		if (Input.GetKey(KeyCode.LeftArrow)) {
  			dpad.x = -1.0f;
  		} else if (Input.GetKey(KeyCode.RightArrow)) {
  			dpad.x = 1.0f;
  		}

  	


		if (inputState.dpad != dpad) {
			inputState.dpad = dpad;
			Messenger.Broadcast("dpad", inputState);
		}

	}
	
	void TriggerShoulderButtons()
	{

		/*
		bool r1 = Input.GetButton("logitech_r1");
		float r2 = Input.GetAxis("logitech_r2");
		bool l1 = Input.GetButton("logitech_l1");
		float l2 = Input.GetAxis("logitech_l2");

		if (inputState.R1 != r1) {
			inputState.R1 = r1;
			Messenger.Broadcast("r1", inputState);
		}

		if (inputState.R2 != r2) {
			inputState.R2 = r2;
			Messenger.Broadcast("r2", inputState);
		}

		if (inputState.L1!= l1) {
			inputState.L1 = l1;
			Messenger.Broadcast("l1", inputState);

		}

		if (inputState.L2 != l2) {
			inputState.L2 = l2;
			Messenger.Broadcast("l2", inputState);
		}
		*/

	}

	#endif
}

