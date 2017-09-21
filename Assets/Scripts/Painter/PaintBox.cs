using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PaintBox {


	public Color32 brush = new Color32();

	// indices are values for {TL, TR, BL, BR, T, L, R, B, CodePage}

	public int[,] styles = new int[7,9] {
		{218,191,192,217,196,179,179,196,0},
		{201,187,200,188,205,186,186,205,0},
		{96,98,128,130,111,127,127,111,1},
		{99, 101,131,133,100,115,117,132,1},
		{102,104,134,136,103,118,120,135,1},
		{105,107,137,139,106,121,123,138,1},
		{144,146,176,178,145,160,162,177,1},
	};

	public int currentStyle = 0;


	public bool preview = false;

	public Rect r;

	public PaintBox() {
		brush = Screen.GenerateBrush();
	}

	public void SetTopLeft(Vector2 v) {
		r.position = v;
	}

	public void SetBottomRight(Vector2 v) {

		r.width = v.x - r.position.x;
		r.height = v.y - r.position.y;

	}

	public void Draw() {
		if (preview) Preview ();
	}

	public void Preview() {
		DrawIntoLayer ();
	}

	public void Commit() {

		DrawIntoLayer (true);

	}

	void DrawIntoLayer(bool permanent = false) {


		int fg = brush.g % 64;
		int bg = brush.b % 64;
		int cp = styles [currentStyle, 8];

		Rect s = r;

		Vector2 TL = new Vector2(Mathf.Min (s.xMin, s.xMax),  Mathf.Min (s.yMin, s.yMax));

		s.position = TL;
		s.width = Mathf.Abs (s.width);
		s.height = Mathf.Abs (s.height);

		// draw H and V lines

		if (permanent) {
			for (uint y = (uint)s.yMin; y <= (uint)s.yMax; y++) {

				int chr = styles [currentStyle, 5];

				Painter.SetPixel ((uint)s.xMin, y, Screen.GenerateBrush (fg, bg, chr, cp));

				chr = styles [currentStyle, 6];

				Painter.SetPixel ((uint)s.xMax, y, Screen.GenerateBrush (fg, bg, chr, cp));

			}
			for (uint x = (uint)s.xMin; x <= (uint)s.xMax; x++) {

				int chr = styles [currentStyle, 4];

				Painter.SetPixel (x, (uint)s.yMin, Screen.GenerateBrush (fg, bg, chr, cp));

				chr = styles [currentStyle, 7];

				Painter.SetPixel (x, (uint)s.yMax, Screen.GenerateBrush (fg, bg, chr, cp));

			}

			// draw corner pieces

			Painter.SetPixel ((uint)s.xMin, (uint)s.yMin, Screen.GenerateBrush (fg, bg, styles [currentStyle, 0], cp));
			Painter.SetPixel ((uint)s.xMax, (uint)s.yMin, Screen.GenerateBrush (fg, bg, styles [currentStyle, 1], cp));
			Painter.SetPixel ((uint)s.xMin, (uint)s.yMax, Screen.GenerateBrush (fg, bg, styles [currentStyle, 2], cp));
			Painter.SetPixel ((uint)s.xMax, (uint)s.yMax, Screen.GenerateBrush (fg, bg, styles [currentStyle, 3], cp));



		} else {


			Screen.Layer layer = Screen.Layer.FLOATING;


			for (uint y = (uint)s.yMin; y <= (uint)s.yMax; y++) {

				int chr = styles [currentStyle, 5];

				Screen.SetPixel ((uint)s.xMin, y, Screen.GenerateBrush (fg, bg, chr, cp), layer);

				chr = styles [currentStyle, 6];

				Screen.SetPixel ((uint)s.xMax, y, Screen.GenerateBrush (fg, bg, chr, cp), layer);

			}
			for (uint x = (uint)s.xMin; x <= (uint)s.xMax; x++) {

				int chr = styles [currentStyle, 4];

				Screen.SetPixel (x, (uint)s.yMin, Screen.GenerateBrush (fg, bg, chr, cp), layer);

				chr = styles [currentStyle, 7];

				Screen.SetPixel (x, (uint)s.yMax, Screen.GenerateBrush (fg, bg, chr, cp), layer);

			}


			// draw corner pieces

			Screen.SetPixel ((uint)s.xMin , (uint)s.yMin, Screen.GenerateBrush(fg,bg,styles [currentStyle, 0],cp), layer);
			Screen.SetPixel ((uint)s.xMax , (uint)s.yMin, Screen.GenerateBrush(fg,bg,styles [currentStyle, 1],cp), layer);
			Screen.SetPixel ((uint)s.xMin , (uint)s.yMax, Screen.GenerateBrush(fg,bg,styles [currentStyle, 2],cp), layer);
			Screen.SetPixel ((uint)s.xMax , (uint)s.yMax, Screen.GenerateBrush(fg,bg,styles [currentStyle, 3],cp), layer);

			string toolTip = s.width + "x" + s.height;

			GUI.DrawString (new Vector2(s.xMax - toolTip.Length , s.yMax - 1), toolTip , Screen.GenerateBrush());


		}
	}

}