using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Pix  {
	
	public Texture2D texture;
	Color32[] buffer;



	public static Pix Load(string fileName)
	{

		Pix p = new Pix ();

		p.LoadFile (fileName);

		return p;

	}

	public void Draw(Vector2 loc) {

		for (int y=0; y<texture.height; y++) {
			for (int x = 0; x < texture.width; x++) {
				Color32 c = buffer [x + y * texture.width];

				Screen.SetPixel ((uint)(x + loc.x), (uint)(y + loc.y), c, Screen.Layer.FLOATING);

			}
		}

	}


	public void LoadFile(string fileName) {
		
		texture = new Texture2D((int)8, (int)8, TextureFormat.ARGB32, false);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;

		byte[] fileData;
		Debug.Log ("trying to load pix: " + fileName);
		if (File.Exists(Application.dataPath + "/Resources/Pix/" + fileName))
		{
			Debug.Log("found " + Application.dataPath + "/Resources/Pix/" + fileName);
			fileData = File.ReadAllBytes(Application.dataPath + "/Resources/Pix/" + fileName);
			texture.LoadImage(fileData); //..this will auto-resize the texture dimensions.
			texture.Apply();

			buffer = new Color32[(int)(texture.width * texture.height)];
			buffer = texture.GetPixels32();

		}
	}



}
