using UnityEngine;
using System.Collections;

public class GUIFrame : GUIElement {

	public bool useBorder = true;

    public override void LocalDraw()
    {
		if (useBorder) {
			GUI.DrawBorderBox(globalRect);
		} else {
			GUI.DrawBox(globalRect);
		}
    }

	protected override void Select()
    {
        if (rootElement!=null)
            rootElement.Activate();
    }

}
