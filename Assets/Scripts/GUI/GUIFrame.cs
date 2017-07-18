using UnityEngine;
using System.Collections;

public class GUIFrame : GUIElement {

    public override void LocalDraw()
    {
        GUI.DrawBorderBox(globalRect);
    }

    protected override void Select()
    {
        if (rootElement!=null)
            rootElement.Activate();
    }


}
