using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public delegate void StringInputDelegate(string s);


public class StringInputDialog : ScreenComponent {

	public static StringInputDialog instance;

    static GUIElement inspector;

	public static event StringInputDelegate stringInputDelegate;

    void Awake()
    {
        active = false;
        instance = this;
    }



	public static void OpenStringInputDialog()
	{
		inspector = instance.gameObject.AddComponent<GUIFrame>();
		inspector.rect = new Rect(1, 4, 30, 3);
		GUIElement.rootElement = inspector;

		GUITextEntry textEntry = (GUITextEntry)inspector.AddChild<GUITextEntry>();
		textEntry.text = "Enter Filename";
		textEntry.rect = new Rect(1, 1, 30, 1);

		textEntry.textEntrySubmit += SaveCallback;

		textEntry.Activate ();
	}

	public static void SaveCallback(string s) {
		Debug.Log ("user has asked to save to file: "+s);

		stringInputDelegate (s);
	}



    void DestroyWindow()
    {

  
        Destroy(inspector);
    }

    public static void Activate()
    {
        instance.active = true;

		OpenStringInputDialog();
    }

    public static void Deactivate()
    {
        instance.active = false;

        instance.DestroyWindow();
    }

    public override void ScreenUpdate()
    {
        if (active)
        {
            inspector.Draw(new Vector2(1, 1));
        }

    }

    public override void PrimaryDown(Vector2 pos)
    {

    }

}
