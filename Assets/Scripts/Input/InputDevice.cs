using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputDevice : MonoBehaviour {

    static protected List<InputDevice> allDevices;

    virtual public void Tick() { }

    public static void PollDevices()
    {
		if (allDevices != null) {
			
			foreach (InputDevice inputDevice in allDevices) {
				inputDevice.Tick ();
			}
		}
    }
}
