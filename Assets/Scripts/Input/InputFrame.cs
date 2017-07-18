using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputFrame : MonoBehaviour {

	private static List<InputFrame> frames;
	public static InputFrame currentFrame;
    public static InputFrame lastFrame;

	protected bool active = false;

    static int c= 0;

    protected virtual void Awake() {
		if (frames==null) {
			frames = new List<InputFrame>();
		}

        if (!frames.Contains(this)) {
            frames.Add(this);
        }

    }

    public static void DeactivateAll() {
		for (int i=0; i<frames.Count; i++) {
			if (frames[i].active) frames[i].Deactivate();
		}
	}

    public void ActivateLast()
    {
        if (lastFrame != null) lastFrame.Activate();
    }

    public virtual void Activate() {

        Debug.Log("activing [" + this + "] input frame, last frame is: "+currentFrame);

        lastFrame = currentFrame;

        DeactivateAll();

        currentFrame = this;

        if (active) return;

		Messenger.AddListener<InputState>("leftStick", OnLeftStick);
		Messenger.AddListener<InputState>("rightStick", OnRightStick);
		Messenger.AddListener<InputState>("leftStickDown", OnLeftStickDown);
		Messenger.AddListener<InputState>("leftStickUp", OnLeftStickUp);	
		Messenger.AddListener<InputState>("rightStickDown", OnRightStickDown);
		Messenger.AddListener<InputState>("rightStickUp", OnRightStickUp);			
		Messenger.AddListener<InputState>("dpad", OnDpad);
		Messenger.AddListener<InputState>("buttonCrossDown", OnButtonCrossDown);
		Messenger.AddListener<InputState>("buttonCrossUp", OnButtonCrossUp);
		Messenger.AddListener<InputState>("buttonCircleDown", OnButtonCircleDown);
		Messenger.AddListener<InputState>("buttonCircleUp", OnButtonCircleUp);
		Messenger.AddListener<InputState>("buttonSquareDown", OnButtonSquareDown);
		Messenger.AddListener<InputState>("buttonSquareUp", OnButtonSquareUp);	
		Messenger.AddListener<InputState>("buttonTriangleDown", OnButtonTriangleDown);
		Messenger.AddListener<InputState>("buttonTriangleUp", OnButtonTriangleUp);	
		Messenger.AddListener<InputState>("buttonOptionDown", OnButtonOptionDown);
		Messenger.AddListener<InputState>("buttonOptionUp", OnButtonOptionUp);
		Messenger.AddListener<InputState>("r1", OnR1);
		Messenger.AddListener<InputState>("r2", OnR2);
		Messenger.AddListener<InputState>("l1", OnL1);
		Messenger.AddListener<InputState>("l2", OnL2);

        c++;


        active = true;


    }

    protected Vector2 QuantizeStickInput(Vector2 input)
    {
        Vector2 result;
        float yabs = Mathf.Abs(input.y);
        float xabs = Mathf.Abs(input.x);

        float ydir = input.y / yabs;
        float xdir = input.x / xabs;

        result.y = yabs > 0.5 ? ydir : 0;
        result.x = xabs > 0.5 ? xdir : 0;

        return result;
    }

    public static void PollMouse()
    {
        if (currentFrame != null)
            currentFrame.CheckMouse();

    }

    public static void PollKeyboard()
    {
        if (currentFrame != null)
            currentFrame.CheckKeyboard();
    }

    public virtual void CheckKeyboard() { }

    public virtual void CheckMouse() { }

    public virtual bool PrimaryDown(Vector2 pos) { return false; }
    public virtual bool PrimaryDrag(Vector2 pos) { return false; }
    public virtual bool PrimaryUp() { return false; }

    public virtual bool SecondaryDown(Vector2 pos) { return false; }
    public virtual bool SecondaryDrag(Vector2 pos) { return false; }
    public virtual bool SecondaryUp() { return false; }

    public virtual void Deactivate() {

        if (!active) return;

		Messenger.RemoveListener<InputState>("leftStick", OnLeftStick);
		Messenger.RemoveListener<InputState>("rightStick", OnRightStick);
		Messenger.RemoveListener<InputState>("leftStickDown", OnLeftStickDown);
		Messenger.RemoveListener<InputState>("leftStickUp", OnLeftStickUp);	
		Messenger.RemoveListener<InputState>("rightStickDown", OnRightStickDown);
		Messenger.RemoveListener<InputState>("rightStickUp", OnRightStickUp);			
		Messenger.RemoveListener<InputState>("dpad", OnDpad);
		Messenger.RemoveListener<InputState>("buttonCrossDown", OnButtonCrossDown);
		Messenger.RemoveListener<InputState>("buttonCrossUp", OnButtonCrossUp);
		Messenger.RemoveListener<InputState>("buttonCircleDown", OnButtonCircleDown);
		Messenger.RemoveListener<InputState>("buttonCircleUp", OnButtonCircleUp);
		Messenger.RemoveListener<InputState>("buttonSquareDown", OnButtonSquareDown);
		Messenger.RemoveListener<InputState>("buttonSquareUp", OnButtonSquareUp);	
		Messenger.RemoveListener<InputState>("buttonTriangleDown", OnButtonTriangleDown);
		Messenger.RemoveListener<InputState>("buttonTriangleUp", OnButtonTriangleUp);	
		Messenger.RemoveListener<InputState>("buttonOptionDown", OnButtonOptionDown);
		Messenger.RemoveListener<InputState>("buttonOptionUp", OnButtonOptionUp);	
		Messenger.RemoveListener<InputState>("r1", OnR1);
		Messenger.RemoveListener<InputState>("r2", OnR2);
		Messenger.RemoveListener<InputState>("l1", OnL1);
		Messenger.RemoveListener<InputState>("l2", OnL2);

        c--;

        active = false;

    }


    //------------------------------------------------------------------------
    // Standard Input Callbacks

    public virtual void OnRightStick(InputState inputState) {}
	public virtual void OnLeftStick(InputState inputState) {}
	public virtual void OnLeftStickDown(InputState inputState) {}
	public virtual void OnLeftStickUp(InputState inputState) {}
	public virtual void OnRightStickDown(InputState inputState) {}
	public virtual void OnRightStickUp(InputState inputState) {}	
	public virtual void OnDpad(InputState inputState) {}
	public virtual void OnButtonCrossDown(InputState inputState) {}
	public virtual void OnButtonCrossUp(InputState inputState) {}
	public virtual void OnButtonCircleDown(InputState inputState) {}
	public virtual void OnButtonCircleUp(InputState inputState) {}
	public virtual void OnButtonSquareDown(InputState inputState) {}
	public virtual void OnButtonSquareUp(InputState inputState) {}	
	public virtual void OnButtonTriangleDown(InputState inputState) {}
	public virtual void OnButtonTriangleUp(InputState inputState) {}	
	public virtual void OnButtonOptionDown(InputState inputState) {}
	public virtual void OnButtonOptionUp(InputState inputState) {}	
	public virtual void OnR1(InputState inputState) {}	
	public virtual void OnR2(InputState inputState) {}	
	public virtual void OnL1(InputState inputState) {}	
	public virtual void OnL2(InputState inputState) {}	

    public virtual void OnDisable()
    {
        if (active) Deactivate();
    }

    public virtual void OnDestroy() {
		if (active) Deactivate();
        if (frames.Contains(this)) frames.Remove(this);
    }
}