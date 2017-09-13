using UnityEngine;
using System.Collections;

public delegate void SwatchSelect(int index, bool foreground);

public class GUISwatch : GUIElement
{

	Color32 brush;

	public event SwatchSelect swatchSelect;

	public int color = 60;

	public bool fg = false;
	public bool bg = false;

	public void Start()
	{
		brush = Screen.GenerateBrush();
	}


	public override void LocalDraw()
	{
		if (fg && bg) {
			brush = Screen.GenerateBrush (63, color, 'X', 0);
		} else if (fg) {
			brush = Screen.GenerateBrush (63, color, 'f', 0);
		} else if (bg) {
			brush = Screen.GenerateBrush (63, color, 'b', 0);
		} else {
			brush = Screen.GenerateBrush (63, color, ' ', 0);
		}

		GUI.SetPixel((int)globalRect.xMin, (int)globalRect.yMin, brush);
	}

	protected override void Select()
	{
		swatchSelect(color, true);
	}

	protected override void SecondarySelect() {
		swatchSelect(color, false);
	}


}
