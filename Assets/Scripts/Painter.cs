using UnityEngine;
using System.Collections;

public class Painter : ScreenComponent {

    public static Painter instance;
    public static PainterInputFrame inputFrame;

    public static Color32 paintColor = new Color(0, 60, '#');

    public static int fg;
    public static int bg;
    public static int chr;
    public static int cp;


    static int brushSize = 1;
    public bool dirty = false;

    static bool splatter = false;

    static Color32[] brushes;
    static int brushPage = 0;

    static int brushIndex;

    public Vector4 alpha = new Vector4(0.01f, 0.44f, 0.14f, 0.18f);
    public Vector4 beta = new Vector4(0.3f, 0.35f, 2.1f, 0.31f);
    public Vector4 theta = new Vector4(0.67f, 0.1f, 0.1f, 0.1f);

    void Awake ()
    {
        instance = this;
        inputFrame = gameObject.AddComponent<PainterInputFrame>();

        paintColor = Screen.GenerateBrush(60, 0, '#');
        active = false;

        brushes = new Color32[100];

        GetSavedBrushes();

        fg = brushes[0].g % 64;
        bg = brushes[0].b %64;
        chr = brushes[0].r;
        cp = brushes[0].g /64;
    }


    void OnSaved()
    {
        dirty = false;
    }

    public static void Activate()
    {
        Debug.Log("activating painter...");
        instance.active = true;
        inputFrame.Activate();
    }

    public static void Deactivate()
    {
        Debug.Log("deactivating painter");
        instance.active = false;
        PlayerInputFrame.instance.Activate();

    }

 
    public void Clear()
    {
        Screen.ClearTexture();
    }

    public static void ToggleSplatter()
    {
        splatter = !splatter;
    }

    public static void IncreaseBrushSize()
    {

        brushSize++;
        brushSize = Mathf.Min(brushSize, 25);

    }

    public static void DecreaseBrushSize()
    {
        brushSize--;
        brushSize = Mathf.Max(brushSize, 1);
    }

    public static void SetBrushIndex(int newIndex)
    {
        brushIndex = newIndex;
    }

    public static void SetBrushPage(int newPage)
    {
        brushPage = newPage;
    }

    public override void ScreenUpdate () {

        if (!active) return;

        /*
         * 
         * // draw grass

        char[] thisChar = { ' ', '|', '/', '/', '/' };

        for (int x=0; x<Screen.instance.dims.x;x++)
        {
            for (int y = 0; y < Screen.instance.dims.y;y++)
            {
                float a = Mathf.PerlinNoise(-x * alpha.x + Time.time * alpha.y, y * alpha.z + Time.time * alpha.w)+1;
                float m = Mathf.PerlinNoise(-x * beta.x + beta.y, y * beta.z + beta.w) - theta.x;

                int b = (int)Mathf.Max(0,Mathf.Min(thisChar.Length-1, (a*m * 9.0f)));

                Color pc = GUI.GenColor(44, 45);
                pc.r =  (float)thisChar[b] / 256.0f;
                Screen.instance.texture.SetPixel(x, y, pc);
            }

        }
        
        Screen.instance.texture.Apply();

        */


        // DRAW BRUSH BAR

        DrawBrushBar();


        if (dirty)
            Landscape.UpdateTextureFromBuffer();


        if (!Palette.instance.active)
        {
            dirty = true;
            uint offset = (uint)(brushSize / 2);
            for (uint x = 0; x < brushSize; x++)
            {
                for (uint y = 0; y < brushSize; y++)
                {
                    if (splatter)
                    {
                        if (Random.value > 0.9)
                            Screen.SetPixel((uint)Screen.pointerPos.x + x - offset, (uint)Screen.pointerPos.y + y - offset, GetPaintBrush(), Screen.Layer.FLOATING);
                    }
                    else
                    {
                        Screen.SetPixel((uint)Screen.pointerPos.x + x - offset, (uint)Screen.pointerPos.y + y - offset, GetPaintBrush(),Screen.Layer.FLOATING);
                    }
                }
            }
        }

    }


    void DrawBrushBar()
    {
        GUI.DrawBox(0, 0, 66, 2, Screen.GenerateBrush());
        GUI.DrawString((int)0, 0, "Pg"+(brushPage+1).ToString(), Screen.GenerateBrush(35,1));

        for (int i = 0; i < 10; i++)
        {

            uint x = (uint)(5 + (i * 4));

            Color32 brush = Screen.GenerateBrush(60);
            if (brushIndex == i)
            {
                brush = Screen.GenerateBrush(63);
            }
            int slot = (i + (brushPage * 10));

            GUI.DrawString((int)x, 0, ((i + 1)%10).ToString(), brush);
            Screen.SetPixel(x + 1, 0, brushes[slot], Screen.Layer.FLOATING);

        }

    }

    public static void GetSavedBrushes()
    {
        for (int i=0; i<100; i++)
            if (PlayerPrefs.HasKey("Brush"+i))
                brushes[i] = HexToColor(PlayerPrefs.GetString("Brush" + i));
    }

    public static void UpdateCurrentBrush()
    {
        SetCurrentBrush(Screen.GenerateBrush(fg, bg, chr, cp));
        Debug.Log(ColorToHex(brushes[brushIndex]));
    }

    public static void SetCurrentBrush(Color32 newBrush)
    {
        int slot = (brushIndex + (brushPage * 10));

        brushes[slot] = newBrush;
        PlayerPrefs.SetString("Brush"+slot, ColorToHex(newBrush));
        PlayerPrefs.Save();
    }


    public static Color32 GetPaintBrush()
    {
        return brushes[brushIndex];

    }

    void Paint(Vector3 pos)
    {
        dirty = true;
        uint offset = (uint)(brushSize / 2);
        for (uint x=0; x<brushSize; x++)
        {
            for (uint y=0; y<brushSize; y++)
            {
                if (splatter)
                {
                    if (Random.value>0.9)
                        SetPixel((uint)pos.x + x -offset, (uint)pos.y + y - offset, GetPaintBrush());
                }
                else
                {
                    SetPixel((uint)pos.x + x - offset, (uint)pos.y + y - offset, GetPaintBrush());
                }
            }
        }
    }

    public void SetPixel(uint x, uint y, Color32 brush)
    {
        //        Screen.SetPixel(x,y, brush);

        Landscape.SetPixel(x + (uint)World.view.x, y + (uint)World.view.y, brush);
    }

    public static void SaveImage()
    {
        Landscape.SaveStaticZone();
    }

    public override void PrimaryDown(Vector2 pos)
    {
        Paint(pos);
    }

    public override void PrimaryDrag(Vector2 pos)
    {
        Paint(pos);
    }

    public override void PrimaryUp()
    {

    }

    public override void SecondaryDown(Vector2 pos)
    {
        Color32 sample = Landscape.GetPixel((uint)Screen.pointerPos.x + (uint)World.view.x, (uint)Screen.pointerPos.y + (uint)World.view.y);


        SetCurrentBrush(sample);

    }


    public static string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

}
