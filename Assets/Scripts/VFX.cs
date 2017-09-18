using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX {

	public class VFXBase {

		public static List<VFXBase> allvfx = new List<VFXBase>();

		int frame = 0;

		public Vector2 location;

		public VFXBase(Vector2 loc) {
			location = loc;
			allvfx.Add (this);
		}

		public bool Tick() {
			frame++;
			return Tick (frame);
		}

		public virtual bool Tick(int frame) {
			Vector2 loc = Screen.GetWorldPixelInScreenSpace(location);

			Screen.SetPixel (loc, Screen.GenerateBrush(63, frame, '/', 0), Screen.Layer.FLOATING);

			return frame < 5;
		}

		public static void Draw() {

			for (int i = allvfx.Count - 1; i >= 0; i--)
			{
				if (!allvfx [i].Tick ()) {
					allvfx.RemoveAt(i);
				}

			}


		}

	}

	public class Whiff : VFXBase {

		public Whiff(Vector2 location) : base(location)	{
		}

		int[] characters = new int[]{'-',' '};
		int[] codepages = new int[]{ 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0 };

		public override bool Tick(int frame) {
			Vector2 loc = Screen.GetWorldPixelInScreenSpace(location);

			int index = frame / 4;

			Color32 c = Screen.GenerateBrush (61, 0, characters[index], codepages[index]);

			Screen.SetPixel (loc, c, Screen.Layer.FLOATING);

			return index < characters.Length-1;

		}


	}



	public class MeleeAttack : VFXBase {
		bool reverse = false;

		public MeleeAttack(Vector2 location) : base(location)	{
			reverse = Random.value > 0.5;
		}

		public override bool Tick(int frame) {
			Vector2 loc = Screen.GetWorldPixelInScreenSpace(location);

			Vector2 a = reverse ? new Vector2 (1, -1) : new Vector2 (1, 1);
			Vector2 b = reverse ? new Vector2 (-1, 1) : new Vector2 (-1, -1);
			int c = reverse ? '/' : '\\';

			switch (frame) {
			case 0:
				{
					Screen.SetPixel (loc + a, 	Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);
					break;
				}
			case 2:
				{
					Screen.SetPixel (loc + a, 	Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);
					break;
				}
			case 3:
				{
					Screen.SetPixel (loc + a, 	Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);
					Screen.SetPixel (loc, 		Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);


					break;
				}
			case 4:
				{

					Screen.SetPixel (loc + a, 	Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);
					Screen.SetPixel (loc, 		Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);
					Screen.SetPixel (loc + b, 	Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);

					break;
				}
			case 5:
				{
					Screen.SetPixel (loc, 		Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);
					Screen.SetPixel (loc + b, 	Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);

					break;
				}
			case 6:
				{

					Screen.SetPixel (loc + b, 	Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);

					break;
				}
			case 8:
			case 7:
				{
					Screen.SetPixel (loc + b, 	Screen.GenerateBrush (63, frame,c, 0), Screen.Layer.FLOATING);

					break;
				}
			case 10:
				{
					return false;
				}
			}


			return true;

		}


	}



	public class Floater : VFXBase {
		bool reverse = false;

		string displayString;

		int[] colors = new int[]{63,35,35,36,36,36,27,27,27,27,27,27,27,26,26,26,26,26,26,26,9};
		int[] bgs = new int[]{27,26,26,9,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

		public Floater(Vector2 location, string s) : base(location)	{
			displayString = s;
		}

		public override bool Tick(int frame) {
			Vector2 loc = Screen.GetWorldPixelInScreenSpace(location);

			loc.y -= 1;

			if (0==frame % 10) {
				location.y--;
			}

			Color32 c = Screen.GenerateBrush (colors[frame], bgs[frame]);

			GUI.DrawString((int)loc.x, (int)loc.y, displayString, c);

			return frame < colors.Length-1;

		}


	}

}
