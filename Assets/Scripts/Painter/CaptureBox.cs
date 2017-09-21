using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

class CaptureBox : PaintBox {

	public static CaptureBox instance;
	static public Texture2D texture;


	public CaptureBox() {
		
		brush = Screen.GenerateBrush(63,4);

		instance = this;

	}


	public void Review() {

	}

	public void SaveCapture(string s) {

		r.width += 1;
		r.height += 1;
		texture = new Texture2D((int)r.width, (int)r.height, TextureFormat.ARGB32, false);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;


		Color32[] buffer = new Color32[(int)(r.width * r.height)];

		Vector2 address;

		for (int y = (int)r.yMin; y < (int)r.yMax; y++) {
			for (int x = (int)r.xMin; x < (int)r.xMax; x++) {

				address = new Vector2(x,y)+(Vector2)World.view.position;

				Color32 c = Landscape.GetPixel (address);

				buffer [(x-(int)r.xMin) + (y-(int)r.yMin) * (int)r.width] = c;
			}
		}

		texture.SetPixels32 (buffer);
		texture.Apply();

		byte[] bytes = texture.EncodeToPNG();
		Debug.Log("Writing to texture: " + s );
		File.WriteAllBytes(Application.dataPath + "/Resources/Pix/" + s, bytes);


		r.width -= 1;
		r.height -= 1;
	}


}