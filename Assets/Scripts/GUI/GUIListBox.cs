using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GUIListBox : GUIElement
{

    Color32 brush;
    Color32 upArrowBrush;
    Color32 downArrowBrush;

    List<string> items;

    List<GUIListBoxItem> displayItems;

    int index = -1;
    int scroll = 0;
    int totalLines = 0;
	int halfHeight = 0;


	public event ListBoxSelect listBoxCallback;

    public override void Start()
    {
        base.Start();
        upArrowBrush = Screen.GenerateBrush(35, 32, 30, 0);
        downArrowBrush = Screen.GenerateBrush(35, 32, 31, 0);
    }

	public void SetItems(List<string> itemsToShow)
    {
        items = itemsToShow;

        totalLines = Mathf.Min(items.Count, (int)rect.height - 2);
        halfHeight = totalLines / 2;


        displayItems = new List<GUIListBoxItem>();

        GUIListBoxItem t;

        for (int i = 0; i < totalLines; i++) {
            t = (GUIListBoxItem)AddChild<GUIListBoxItem>();
            t.rect = new Rect(1, i + 1, globalRect.width - 2, 1);
            t.text = i.ToString();
            t.index = i;
            t.listBoxSelect += ListBoxSelect;
            displayItems.Add(t);
        }

		//callback = callbackAction;

    }

    void ListBoxSelect(int selectIndex)
    {
        index = selectIndex;

		if (listBoxCallback != null) {
			listBoxCallback (selectIndex);
			//callback (selectIndex);
		}

        if (!active)
        {
            Activate();
        }
    }

 
 

    protected override void Cancel()
    {
        Hide();
    }

    public void Hide()
    {
        ActivateLast();
    }

    public void Update()
    {
        KeyRepeat();
    }



    public override void LocalDraw()
    {

        brush = Screen.GenerateBrush(63, 0);

        if (active)
            GUI.DrawBorderBox(globalRect, 63, 0);
        else
            GUI.DrawBorderBox(globalRect, 59, 0);


        Color32 normalBrush = Screen.GenerateBrush(33, 32);
        Color32 highlightBrush = Screen.GenerateBrush(34, 32);

        for (int i=0; i< totalLines; i++)
        {
            int showIndex = i + scroll;
            if (showIndex<items.Count)
            {
                displayItems[i].index = showIndex;
                displayItems[i].text = items[showIndex];

                if (showIndex == index)
                {
                    displayItems[i].fg = 63;
                 //   GUI.DrawString((int)globalRect.xMin+1, (int)globalRect.yMin + i + 1, items[showIndex], highlightBrush);
                }
                else
                {
                    displayItems[i].fg = 60;

                    //   GUI.DrawString((int)globalRect.xMin + 1, (int)globalRect.yMin + i + 1, items[showIndex], normalBrush);
                }
            }


        }

        if (scroll>0)
        {
            Screen.SetPixel((uint)(globalRect.center.x ), (uint)globalRect.yMin, upArrowBrush, Screen.Layer.FLOATING);
        }
        if (scroll<items.Count-totalLines)
        {
            Screen.SetPixel((uint)(globalRect.center.x ), (uint)(globalRect.yMax-1), downArrowBrush, Screen.Layer.FLOATING);
        }


        
    }


    protected override void ProcessInputState()
    {
        if (guiState == null) return;

        if (guiState.dpad.y > 0)
        {
            if (index > 0)
            {
                index--;
                if (index < scroll)
                    scroll = index;

            }
        }
        else if (guiState.dpad.y < 0)
        {
            if (index < items.Count - 1)
            {
                index++;
                if (index > scroll+totalLines-1)
                    scroll = (index-totalLines)+1;

                scroll = Mathf.Min(scroll, (items.Count - totalLines));


            }
        }
    }


    public override void CheckKeyboard()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) { Cancel(); }
        if (Input.GetKeyDown(KeyCode.Return)) { Select();  }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { guiState.dpad.y = 1; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { guiState.dpad.y = -1; }
        if (Input.GetKeyUp(KeyCode.UpArrow)) { guiState.dpad.y = 0; }
        if (Input.GetKeyUp(KeyCode.DownArrow)) { guiState.dpad.y = 0; }


        if (Input.GetKeyDown(KeyCode.Backspace)) { }
        if (Input.GetKeyDown(KeyCode.Delete)) { }
        if (Input.GetKeyDown(KeyCode.Tab)) { }
        if (Input.GetKeyDown(KeyCode.Pause)) { }
        if (Input.GetKeyDown(KeyCode.Backspace)) { }
        if (Input.GetKeyDown(KeyCode.Space)) { }
        if (Input.GetKeyDown(KeyCode.Keypad0)) { }
        if (Input.GetKeyDown(KeyCode.Keypad1)) { }
        if (Input.GetKeyDown(KeyCode.Keypad2)) { }
        if (Input.GetKeyDown(KeyCode.Keypad3)) { }
        if (Input.GetKeyDown(KeyCode.Keypad4)) { }
        if (Input.GetKeyDown(KeyCode.Keypad5)) { }
        if (Input.GetKeyDown(KeyCode.Keypad6)) { }
        if (Input.GetKeyDown(KeyCode.Keypad8)) { }
        if (Input.GetKeyDown(KeyCode.Keypad9)) { }
        if (Input.GetKeyDown(KeyCode.KeypadPeriod)) { }
        if (Input.GetKeyDown(KeyCode.KeypadDivide)) { }
        if (Input.GetKeyDown(KeyCode.KeypadMultiply)) { }
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) { }
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) { }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) { }
        if (Input.GetKeyDown(KeyCode.KeypadEquals)) { }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { }
        if (Input.GetKeyDown(KeyCode.Insert)) { }
        if (Input.GetKeyDown(KeyCode.Home)) { }
        if (Input.GetKeyDown(KeyCode.End)) { }
        if (Input.GetKeyDown(KeyCode.PageUp)) { }
        if (Input.GetKeyDown(KeyCode.PageDown)) { }
        if (Input.GetKeyDown(KeyCode.F1)) { }
        if (Input.GetKeyDown(KeyCode.F2)) { }
        if (Input.GetKeyDown(KeyCode.F3)) { }
        if (Input.GetKeyDown(KeyCode.F4)) { }
        if (Input.GetKeyDown(KeyCode.F5)) { }
        if (Input.GetKeyDown(KeyCode.F6)) { }
        if (Input.GetKeyDown(KeyCode.F7)) { }
        if (Input.GetKeyDown(KeyCode.F8)) { }
        if (Input.GetKeyDown(KeyCode.F9)) { }
        if (Input.GetKeyDown(KeyCode.F10)) { }
        if (Input.GetKeyDown(KeyCode.F11)) { }
        if (Input.GetKeyDown(KeyCode.F12)) { }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { }
        if (Input.GetKeyDown(KeyCode.Exclaim)) { }
        if (Input.GetKeyDown(KeyCode.DoubleQuote)) { }
        if (Input.GetKeyDown(KeyCode.Hash)) { }
        if (Input.GetKeyDown(KeyCode.Dollar)) { }
        if (Input.GetKeyDown(KeyCode.Ampersand)) { }
        if (Input.GetKeyDown(KeyCode.Quote)) { }
        if (Input.GetKeyDown(KeyCode.LeftParen)) { }
        if (Input.GetKeyDown(KeyCode.RightParen)) { }
        if (Input.GetKeyDown(KeyCode.Asterisk)) { }
        if (Input.GetKeyDown(KeyCode.Plus)) { }
        if (Input.GetKeyDown(KeyCode.Comma)) { }
        if (Input.GetKeyDown(KeyCode.Minus)) { }
        if (Input.GetKeyDown(KeyCode.Period)) { }
        if (Input.GetKeyDown(KeyCode.Slash)) { }
        if (Input.GetKeyDown(KeyCode.Colon)) { }
        if (Input.GetKeyDown(KeyCode.Semicolon)) { }
        if (Input.GetKeyDown(KeyCode.Less)) { }
        if (Input.GetKeyDown(KeyCode.Equals)) { }
        if (Input.GetKeyDown(KeyCode.Greater)) { }
        if (Input.GetKeyDown(KeyCode.Question)) { }
        if (Input.GetKeyDown(KeyCode.At)) { }
        if (Input.GetKeyDown(KeyCode.LeftBracket)) { }
        if (Input.GetKeyDown(KeyCode.Backslash)) { }
        if (Input.GetKeyDown(KeyCode.RightBracket)) { }
        if (Input.GetKeyDown(KeyCode.Caret)) { }
        if (Input.GetKeyDown(KeyCode.Underscore)) { }
        if (Input.GetKeyDown(KeyCode.BackQuote)) { }
        if (Input.GetKeyDown(KeyCode.A)) { }
        if (Input.GetKeyDown(KeyCode.B)) { }
        if (Input.GetKeyDown(KeyCode.C)) { }
        if (Input.GetKeyDown(KeyCode.D)) { }
        if (Input.GetKeyDown(KeyCode.E)) { }
        if (Input.GetKeyDown(KeyCode.F)) { }
        if (Input.GetKeyDown(KeyCode.G)) { }
        if (Input.GetKeyDown(KeyCode.H)) { }
        if (Input.GetKeyDown(KeyCode.I)) { }
        if (Input.GetKeyDown(KeyCode.J)) { }
        if (Input.GetKeyDown(KeyCode.K)) { }
        if (Input.GetKeyDown(KeyCode.L)) { }
        if (Input.GetKeyDown(KeyCode.M)) { }
        if (Input.GetKeyDown(KeyCode.N)) { }
        if (Input.GetKeyDown(KeyCode.O)) { }
        if (Input.GetKeyDown(KeyCode.P)) { }
        if (Input.GetKeyDown(KeyCode.Q)) { }
        if (Input.GetKeyDown(KeyCode.R)) { }
        if (Input.GetKeyDown(KeyCode.S)) { }
        if (Input.GetKeyDown(KeyCode.T)) { }
        if (Input.GetKeyDown(KeyCode.U)) { }
        if (Input.GetKeyDown(KeyCode.V)) { }
        if (Input.GetKeyDown(KeyCode.W)) { }
        if (Input.GetKeyDown(KeyCode.X)) { }
        if (Input.GetKeyDown(KeyCode.Y)) { }
        if (Input.GetKeyDown(KeyCode.Z)) { }


    }
}

