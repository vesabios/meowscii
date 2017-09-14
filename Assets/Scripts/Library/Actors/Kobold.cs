using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kobold : PActor {


	public override void Init ()
	{
		base.Init ();

	}

	public override void Draw() {
		Color32 brush = Screen.GenerateBrush();
		brush.r = (byte)Convert.ToInt32('k');
		Screen.SetWorldPixelInScreenSpace(location, brush, Screen.Layer.FLOATING);
		/*

		for (int y = 0; y < 33; y++) {
			for (int x = 0; x < 60; x++) {

				int u = x + (int)Engine.player.location.x;
				int v = y + (int)Engine.player.location.y;

				Color32 c = Screen.GenerateBrush (dijkstra.graph [u, v], dijkstra.graph [u, v]);
				Screen.SetPixel ((uint)x, (uint)y, c, Screen.Layer.FLOATING);

			}
		}
		*/


	}

}
