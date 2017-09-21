using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }


    public static float map(this float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }


	public static byte fg(this Color32 c) {
		return (byte)(c.g % 64);
	}

	public static byte bg(this Color32 c) {
		return c.b;
	}

	public static byte codepage(this Color32 c) {
		return (byte)(c.g / 64);
	}

	public static byte character(this Color32 c) {
		return c.r;
	}

	/*
	 * 
	 *     public static Color32 GenerateBrush(int fg = 63, int bg = 0, int c = 0, int page = 0)
    {
        byte r = (byte)(c);
        byte g = (byte)(fg + (page * 64));
        byte b = (byte)(bg);
        byte a = 255;
        return new Color32(r,g,b,a);
    }*/


}