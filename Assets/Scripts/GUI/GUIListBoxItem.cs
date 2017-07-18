using UnityEngine;
using System.Collections;

public delegate void ListBoxSelect(int index);

public class GUIListBoxItem : GUIElement
{

    public string text;

    Color32 brush;
    Color32 activeBrush;

    public event ListBoxSelect listBoxSelect;

    public int index;

    public int fg = 60;


    public void Start()
    {
        brush = Screen.GenerateBrush();
    }


    public override void LocalDraw()
    {
        globalRect.width = rect.width = text.Length;

        brush = Screen.GenerateBrush(fg, 0);

        GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, text, brush);

    }

    protected override void Select()
    {
        listBoxSelect(index);
    }

    public override void CheckKeyboard()
    {

    }

}
