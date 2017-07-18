using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Palette : ScreenComponent {

    public static Palette instance;
    Vector2 pos;

    int brushSize;

    Color32 sample;

    static int codepage = 0;


    

    void Awake()
    {
        instance = this;
    }

    public static void IncrementCodePage()
    {
        codepage++;
        if (codepage > 3) codepage = 3;
    }

    public static void DecrementCodePage()
    {
        codepage--;
        if (codepage <= 0) codepage = 0;
    }

	public override void ScreenUpdate () {

        if (active)
        {
            Paint();
        } 
    }

    public static void Toggle()
    {
        if (instance.active) instance.Hide();
        else instance.Show();
    }

    void Paint()
    {

        // draw palette

        //DrawPalette((int)pos.x - 5, (int)pos.y - 5);

        DrawPalette(40, 23);

        sample = Screen.GetPixel(Screen.pointerPos, 1);

        if (sample.a == 0)
        {
            sample = Screen.GetPixel(Screen.pointerPos, 0);
        }


        /*
        GUI.DrawBox(1, 27, 5, 5, new Color(0, sample.g, sample.g));
        GUI.DrawBox(6, 27, 5, 5, new Color(0, sample.b, sample.b));

        GUI.DrawBox(1, 1, 25, 10, Color.black);
        GUI.DrawString(2, 2, "MEOWSCII");
        */

    }

    void DrawPalette(int ix, int iy)
    {
        int ox = (int)Screen.dims.x - 18;
        int oy = 2;

        int index = 0;

        GUI.DrawBorderBox((int)ox, (int)oy, 18, 31, 62,1);

        {
            for (int y=0; y<4; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    int palIndex = (y * 16) + x;

                    Color32 brush = Screen.GenerateBrush(index, index, 0);

                    if (palIndex == Painter.fg)
                        brush = Screen.GenerateBrush(63, index, 'F', Painter.cp);
                    else if (palIndex == Painter.bg)
                        brush = Screen.GenerateBrush(63, index, 'B', Painter.cp);

                    int xx = ox + x + 1;
                    int yy = oy + y + 17;

                    Screen.SetPixel((uint)xx, (uint)yy, brush, Screen.Layer.FLOATING);
                    index++;
                }
            }

        }

        index = 0;
        for (int y = 0; y< 16; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                int charIndex = (int)(y * 16) + x;
                Color32 brush = Screen.GenerateBrush(61, 0, index, codepage);

                if (charIndex == Painter.chr)
                {
                    brush = Screen.GenerateBrush(Painter.fg, Painter.bg, index, codepage);
                } 

                int xx = (int)ox + x + 1;
                int yy = (int)oy + y + 1;

                Screen.SetPixel((uint)xx, (uint)yy, brush, Screen.Layer.FLOATING);

                index++;
            }
        }


        string character = (Painter.chr).ToString();
        string fg = Mathf.Round(Painter.fg).ToString();
        string bg = Mathf.Round(Painter.bg).ToString();

        {
            int x = (int)ox+1;

            GUI.DrawString(x, 24, "FG:" + fg);
            GUI.DrawString(x, 25, "BG:" + bg);
            GUI.DrawString(x, 26, "CH:"+character);
            GUI.DrawString(x, 27, "CP:"+Painter.cp);

        }



    }

    public void Show()
    {
        active = true;
    }

    public void Hide()
    {
        active = false;
    }

    public override void PrimaryDown(Vector2 pos)
    {

        if (pos.x >= Screen.dims.x-18)
        {

            if (pos.y <= 18)
            {
                Painter.chr = sample.r;
                Painter.cp = (byte)codepage;
            }
            else
            {
                Painter.fg = sample.g;
            }

            Painter.UpdateCurrentBrush();
        }

    }

    public override void PrimaryDrag(Vector2 pos)
    {
     

    }

    public override void PrimaryUp()
    {

    }

    public override void SecondaryDown(Vector2 pos)
    {
        if (pos.x >= Screen.dims.x - 18)
        {
            Painter.bg = sample.b;
            Painter.UpdateCurrentBrush();
        } else
        {
            Painter.instance.SecondaryDown(pos);
        }

    }



}
