
using UnityEngine;

#if UNITY_PS4
	using UnityEngine.PS4;
#endif

using System;
using System.Collections;

public class DualShock : MonoBehaviour
{

	public int playerId = -1;

	#if UNITY_PS4

	public InputState inputState = new InputState();
	private int stickID;


	void Start()
	{
		stickID = playerId + 1;
	}

	void Update()
	{
		if(PS4Input.PadIsConnected(playerId))
		{


			Thumbsticks();
			InputButtons();
			DPadButtons();
			TriggerShoulderButtons();

			if(Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button7", true))) {
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

			/*

			// Make the gyro rotate to match the physical controller
			gamePad.gyro.localEulerAngles = new Vector3(-PS4Input.GetLastOrientation(playerId).x,
			                                            -PS4Input.GetLastOrientation(playerId).y,
			                                            PS4Input.GetLastOrientation(playerId).z) * 100;

			*/
														
														
		} else {

		}

	}

	



	void Thumbsticks()
	{
		// Move the thumbsticks around

		Vector2 leftStick = new Vector2(
			Input.GetAxis("leftstick" + stickID + "horizontal"),
			Input.GetAxis("leftstick" + stickID + "vertical")
		);

		Vector2 rightStick = new Vector2(
			Input.GetAxis("rightstick" + stickID + "horizontal"),
			Input.GetAxis("rightstick" + stickID + "vertical")
		);

		if (inputState.leftStick != leftStick) {
			inputState.leftStick = leftStick;
			Messenger.Broadcast("leftStick", inputState);
		}

		if (inputState.rightStick != rightStick) {
			inputState.rightStick = rightStick;
			Messenger.Broadcast("rightStick", inputState);
		}


		if(Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button8", true))) {
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

		if(Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button9", true))) {
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

	}

	// Make the Cross, Circle, Triangle and Square buttons light up when pressed
	void InputButtons()
	{


		if(Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0", true))) {
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

		if(Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1", true))) {
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

		if(Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2", true))) {
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

		if(Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button3", true))) {
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


	}

	// Make the DPad directions light up when pressed
	void DPadButtons()
	{

		Vector2 dpad = Vector2.zero;

		if(Input.GetAxis("dpad" + stickID + "_horizontal") > 0) {
			dpad.x = 1.0f;
		} else if(Input.GetAxis("dpad" + stickID + "_horizontal") < 0) {
			dpad.x = -1.0f;
		} 

		if(Input.GetAxis("dpad" + stickID + "_vertical") > 0) {
			dpad.y = -1.0f;
		} else if(Input.GetAxis("dpad" + stickID + "_vertical") < 0) {
			dpad.y = 1.0f;
		}

		if (inputState.dpad != dpad) {
			inputState.dpad = dpad;
			Messenger.Broadcast("dpad", inputState);
		}



	}
	
	void TriggerShoulderButtons()
	{

		bool r1 = Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button5", true));
		float r2 = Input.GetAxis("joystick" + stickID + "_right_trigger");
		bool l1 = Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button4", true));
		float l2 = Input.GetAxis("joystick" + stickID + "_left_trigger");

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

	}




	#endif
}

