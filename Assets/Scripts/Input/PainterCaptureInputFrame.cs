using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SaveCaptureToFile(string s);


public class PainterCaptureInputFrame : InputFrame {




	public override void Activate() {
		base.Activate ();

		StringInputDialog.Deactivate ();

	}

	public override void CheckKeyboard()
	{
		if (Input.GetKeyDown (KeyCode.S)) {
			StringInputDialog.Activate ();
			StringInputDialog.stringInputDelegate += SaveCallback;

		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			Painter.SetMode (Painter.Mode.MOUSE);
		}
	}


	public void SaveCallback(string s) {

		Debug.Log ("PainterCaptureInputFrame saving " + s);
		StringInputDialog.stringInputDelegate -= SaveCallback;

		CaptureBox.instance.SaveCapture (s);

	}

}
