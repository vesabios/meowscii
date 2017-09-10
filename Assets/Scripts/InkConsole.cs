using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InkConsole : ScreenComponent
{

    public static InkConsole instance;
    public InkInputFrame inkInputFrame;

    public class InkButton
    {
        public string s;
        public Rect r;
        public UnityEngine.Events.UnityAction callback;
    }

    List<InkButton> buttons = new List<InkButton>();
    List<string> strings = new List<string>();

    UnityEngine.Events.UnityAction callback;

    void Awake()
    {
        active = true;
        instance = this;
    }

    /*
    void Update()
    {

        if (Input.GetKeyDown("i"))
        {
            
        }
    }
    */

    public void Toggle()
    {
        active = !active;
    }



    public void AddText(string s)
    {
        strings.Add(s);
    }

    public void ClearButtons()
    {
        buttons.Clear();
    }

    public void AddButton(string s, UnityEngine.Events.UnityAction callback)
    {
        InkButton b = new InkButton();

        b.s = s;
        b.callback = callback;

        buttons.Add(b);

    }


    public void ClearAll()
    {
        buttons = new List<InkButton>();
        strings.Clear();

		Screen.writeIndex = 0;

    }


    public override void ScreenUpdate()
    {
        if (!active) return;
        int y = DrawBuffer();
        DrawButtons(y);

    }

    public int DrawBuffer()
    {
        int y = 3;
        for (int i=0; i<strings.Count; i++)
        {
            y = (int)GUI.DrawStringWrappedBox(3, y, strings[i], Screen.GenerateBrush()).y+1;
        }
        return y+1;

    }

    public void DrawButtons(int y)
    {

		y++;
		//y = 20;

		int rowIncrement = 2;

        callback = null;
        int x = 1;
        int row = 0;
        int maxWidth = 0;
        for (int i = 0; i < buttons.Count; i++)
        {

            if (buttons[i].r.Contains(Screen.pointerPos)) {
				buttons[i].r = GUI.DrawButton(x, y + (row * rowIncrement), buttons[i].s, GUI.ButtonMode.HIGHLIGHT);
                callback = buttons[i].callback;
            }
            else
            {
				buttons[i].r = GUI.DrawButton(x, y + (row * rowIncrement), buttons[i].s, GUI.ButtonMode.NORMAL);
            }

            maxWidth = (int)Mathf.Max(maxWidth, buttons[i].r.width);

            row++;

			if (y+(row*rowIncrement)> 29)
            {
                row = 0;
                x += maxWidth +1;
            }

        }

    }



    public override void PrimaryDown(Vector2 pos)
    {
        if (callback!=null) callback();
    }

}
