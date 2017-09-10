using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ScreenComponent : MonoBehaviour
{
    [HideInInspector]
    public bool active = false;

    public virtual void PrimaryDown(Vector2 pos) { }
    public virtual void PrimaryDrag(Vector2 pos) { }
    public virtual void PrimaryUp() { }

    public virtual void SecondaryDown(Vector2 pos) { }
    public virtual void SecondaryDrag(Vector2 pos) { }
    public virtual void SecondaryUp() { }

    public virtual void ScreenUpdate() { }

    static List<ScreenComponent> components;

    public GUIElement rootElement;


    public void Start()
    {
        if (components == null) components = new List<ScreenComponent>();
        components.Add(this);
    }

    void OnDestroy()
    {
        components.Remove(this);
    }

	public static bool redraw = true;
    public static void DrawAllComponents()
    {	

		//if (!redraw)
		//	return;

        for (int i = components.Count-1; i>=0; i--)
        {
            components[i].ScreenUpdate();
        }
		redraw = false;
    }

    public static ScreenComponent GetTopComponent()
    {
        for (int i = 0; i < components.Count; i++)
        {
            if (components[i].active) return components[i];
        }
        return null;
    }


}
