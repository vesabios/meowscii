﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


public class GUIElement : InputFrame
{
    protected static float KEY_REPEAT = 0.1f;
    protected static float keyRepeatTimer = 0;

    public Rect rect = new Rect(0, 0, 1, 1);
    public Rect globalRect = new Rect(0, 0, 1, 1);

    protected InputState guiState;

    public bool bHandlesInput = true;
    public bool bCanActivate = false;

    public static GUIElement rootElement;

    protected List<GUIElement> children = new List<GUIElement>();

    public FieldInfo field;
    public object objectToInspect;

    protected void KeyRepeat()
    {
        keyRepeatTimer += Time.deltaTime;
        if (keyRepeatTimer > KEY_REPEAT)
        {
            keyRepeatTimer = 0;
            ProcessInputState();
        }
    }

    public bool isWithin(Vector2 testLocation)
    {
        return globalRect.Contains(testLocation);
    }


    public GUIElement AddChild<T>() where T:GUIElement
    {
        GUIElement e = gameObject.AddComponent<T>();
        children.Add(e);
        return e;
    }

    public void RemoveChild(GUIElement element)
    {
        if (children.Contains(element))
        {
            children.Remove(element);
            Destroy(element);
        }

    }

 
    public virtual void Start()
    {
        guiState = new InputState();

    }

    public override void OnDestroy()
    {
        for(int i=children.Count-1; i>=0; i--)
        {
            Destroy(children[i]);
            children.Remove(children[i]);
        }
        base.OnDestroy();
    }

    public void CalculateGlobalRect(Vector2 newOrigin)
    {
        globalRect = rect;
        globalRect.position = rect.position + newOrigin;
    }

    public virtual void LocalDraw() { }

    public void Draw(Vector2 origin)
    {
        CalculateGlobalRect(origin);

        LocalDraw();

        foreach (GUIElement element in children)
        {
            element.Draw(globalRect.position);
        }

    }


    public override bool PrimaryDown(Vector2 pointerPosition)
    {
        foreach(GUIElement element in children)
        {
            if (element.PrimaryDown(pointerPosition)) return true;   // let children catch the mouse press
        }

		// otherwise, if no children pick this up, let's see if this class can pick it up
        if (isWithin(pointerPosition))
        {

			Select();
            return true;
        }

        return false;          
    }


	public override bool SecondaryDown(Vector2 pointerPosition)
	{
		foreach(GUIElement element in children)
		{
			if (element.SecondaryDown(pointerPosition)) return true;   // let children catch the mouse press
		}

		// otherwise, if no children pick this up, let's see if this class can pick it up
		if (isWithin(pointerPosition))
		{

			SecondarySelect();
			return true;
		}

		return false;          
	}





	protected virtual void Select() { }
	protected virtual void SecondarySelect() { }

    protected virtual void Cancel() { }
    protected virtual void ProcessInputState() { }


    public override void OnRightStick(InputState inputState)
    {
        guiState = inputState;
        keyRepeatTimer = 0;
    }

    public override void OnLeftStick(InputState inputState)
    {
        guiState = inputState;
        keyRepeatTimer = 0;
    }

    public override void OnDpad(InputState inputState)
    {
        guiState = inputState;
        keyRepeatTimer = 0;
    }

    public override void OnButtonCircleDown(InputState inputState)
    {
        Cancel();
    }



}