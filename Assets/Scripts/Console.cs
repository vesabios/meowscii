using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour {

	ConsoleInputFrame consoleInputFrame;

	InkManager inkManager;

	void Start () {

		gameObject.AddComponent<InkConsole>();

		gameObject.AddComponent<GUI>();
		consoleInputFrame = gameObject.AddComponent<ConsoleInputFrame>();
		consoleInputFrame.Activate ();


		InkManager.instance.StartStory ();

	}
	
	void Update () {
		

		Screen.PrepareBuffer();

		InputDevice.PollDevices();
		InputFrame.PollKeyboard();
		InputFrame.PollMouse();
		Screen.PollMouse();


		//GUI.DrawString (3, 3, "Road of Ruin", Screen.GenerateBrush());

		ScreenComponent.DrawAllComponents ();

		Screen.DrawBuffer();
	}


	float blinkDelay = 0.2f;
	float blinkTimer = 0.0f;
	bool blinkState = false;
	void DrawCursor(int x, int y) {

		blinkTimer += Time.deltaTime;
		if (blinkTimer > blinkDelay)
			blinkState = !blinkState;
		while (blinkTimer > blinkDelay)
			blinkTimer -= blinkDelay;


		if (blinkState) GUI.DrawString (x, y, System.Convert.ToChar(219).ToString(), Screen.GenerateBrush());


	}
}
