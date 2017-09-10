using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI : ScreenComponent {

    public override void ScreenUpdate()
    {
    }

    public static void DrawBorderBox(Rect r, int fg = 60, int bg = 0)
    {
        DrawBorderBox((int)r.x, (int)r.y, (int)r.width, (int)r.height, fg, bg);
    }
    
    public static void DrawBorderBox(int x, int y, int w, int h, int fg = 60, int bg = 0)
    {
        /*
        105 106 107
        121 122 123
        137 138 139   
        */

        Color32 brush = Screen.GenerateBrush(fg, bg, 0, 1);
        DrawBox(x, y, w, h, brush);

        brush.r = 105;
        Screen.SetPixel(new Vector2(x, y), brush, Screen.Layer.FLOATING);
        brush.r = 107;
        Screen.SetPixel(new Vector2(x+w-1, y), brush, Screen.Layer.FLOATING);
        brush.r = 137;
        Screen.SetPixel(new Vector2(x, y+h-1), brush, Screen.Layer.FLOATING);
        brush.r = 139;
        Screen.SetPixel(new Vector2(x+w-1, y+h-1), brush, Screen.Layer.FLOATING);


        for (int i = 1; i < w-1; i++)
        {
            brush.r = 106;
            Screen.SetPixel(new Vector2(x + i, y), brush, Screen.Layer.FLOATING);
            brush.r = 138;
            Screen.SetPixel(new Vector2(x + i, y+h-1), brush, Screen.Layer.FLOATING);

        }

        for (int i = 1; i < h-1; i++)
        {
            brush.r = 121;
            Screen.SetPixel(new Vector2(x, y + i), brush, Screen.Layer.FLOATING);
            brush.r = 123;
            Screen.SetPixel(new Vector2(x+w-1, y + i), brush, Screen.Layer.FLOATING);

        }
        

    }

    public static void DrawString(int x, int y, string s, Color32 c)
    {
        for (int i = 0; i < s.Length; i++)
        {
            int chr = System.Convert.ToInt32(s[i]);

            c.r = (byte)chr;

            Screen.SetPixel(new Vector2(x + i, y), c, Screen.Layer.FLOATING);
        }
    }


    static Vector2 CalcWrappedHeight(string s)
    {
        int u = 0;
        int v = 0;
        int wrapPoint = 0 + 30;

        int maxU = 0;

        for (int i = 0; i < s.Length; i++)
        {
            int chr = System.Convert.ToInt32(s[i]);
            if (chr != 32) maxU = Mathf.Max(u, maxU);
            u++;
            if (chr == 32)
            {
                int j = i;
                bool breakNeeded = true;
                while (j < s.Length && u < wrapPoint)
                {
                    int test_chr = System.Convert.ToInt32(s[j]);
                    if (test_chr == 32)
                    {
                        breakNeeded = false;
                    }
                    j++;

                }
                if (breakNeeded)
                {
                    u = 0;
                    v++;
                }
            }
        }
        return new Vector2(maxU,v);
    }

    public static Vector2 DrawStringWrappedBox(int x, int y, string s, Color32 brush)
    {
        Vector2 bs = CalcWrappedHeight(s);

        int textHeight = (int)bs.y;
        DrawBox(x, y, (int)bs.x+3, textHeight+3, brush);
        return DrawStringWrapped(x+1, y+1, s, brush);

    }


    public static Vector2 DrawStringWrapped(int x, int y, string s, Color32 brush)
    {
        int u = x;
        int v = y;
        int wrapPoint = x + 30;

        int maxU = 0;

		//int len = Mathf.Min (Screen.writeIndex, s.Length);

		for (int i = 0; i < s.Length; i++)
        {
            int chr = System.Convert.ToInt32(s[i]);
            brush.r = (byte)chr;

			if (i<Screen.writeIndex)  Screen.SetPixel(new Vector2(u, v), brush, Screen.Layer.FLOATING);
            maxU = Mathf.Max(u, maxU);
            u++;
            if (chr==32)
            {
                // we're on a space, let's see if we can fit the next word.
                int j = i;
                bool breakNeeded = true;
				while (j < s.Length && u < wrapPoint) {
                    int test_chr = System.Convert.ToInt32(s[j]);
                    if (test_chr==32)
                    {
                        breakNeeded = false;
                    }
                    j++;

                }
                if (breakNeeded)
                {
                    u = x;
                    v++;
                }
            }
        }
        return new Vector2(maxU,v);
    }



    public static Vector2 DrawStringWrapped(int x, int y, string s)
    {
        Color32 brush = Screen.GenerateBrush(0, 63);
        return DrawStringWrapped(x, y, s, brush);
    }


    public static void DrawString(int x, int y, string s)
    {
        Color32 brush = Screen.GenerateBrush();
        DrawString(x, y, s, brush);
    }

    public static void DrawBox(int x, int y, int w, int h, Color32 brush)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                Screen.SetPixel(new Vector2(x + i, y + j), brush, Screen.Layer.FLOATING);
            }
        }
    }

    public enum ButtonMode
    {
        NULL,
        NORMAL,
        HIGHLIGHT
    }

    public static Rect DrawButton(int x, int y, string s, ButtonMode bm = ButtonMode.NORMAL)
    {

        Color32 brush = Screen.GenerateBrush(14, 2);

        switch (bm) {
            case ButtonMode.NORMAL:
                {
                    brush = Screen.GenerateBrush(62, 59);
                    break;
                }
            case ButtonMode.HIGHLIGHT:
                {
                    brush = Screen.GenerateBrush(63, 58);

                    break;
                }

        }


        DrawBox(x, y, s.Length + 2, 1, brush);
        DrawString(x + 1, y, s, brush);

        return new Rect(x, y, s.Length + 2, 1);
    }

 

}
