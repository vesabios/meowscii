using UnityEngine;
using System.Collections;

public class Painter : ScreenComponent {

	class Cursor {

		public Vector3 position;

		float timer = 0.0f;
		float delay = 0.25f;
		public bool active = true;
		public void Update() {

			timer += Time.deltaTime;
			if (timer > delay) {
				timer -= delay;
				active = !active;
			}

		}

	}

	class PaintBox {

		public Color32 brush = new Color32();

		// indices are ascii codes for TL, TR, BL, BR, T, L, R, B, and codepage, respectively
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
			DrawIntoLayer (Screen.Layer.FLOATING);
		}

		public void Commit() {
			DrawIntoLayer (Screen.Layer.BASE);

		}

		void DrawIntoLayer(Screen.Layer layer) {

			int fg = brush.g % 64;
			int bg = brush.b % 64;
			int cp = styles [currentStyle, 8];

			Rect s = r;

			Vector2 TL = new Vector2(Mathf.Min (s.xMin, s.xMax),  Mathf.Min (s.yMin, s.yMax));

			s.position = TL;
			s.width = Mathf.Abs (s.width);
			s.height = Mathf.Abs (s.height);

			// draw H and V lines

			for (uint y = (uint)s.yMin; y <= (uint)s.yMax; y++) {

				int chr = styles [currentStyle, 5];

				Screen.SetPixel ((uint)s.xMin , y, Screen.GenerateBrush(fg,bg,chr,cp), layer);

				chr = styles [currentStyle, 6];

				Screen.SetPixel ((uint)s.xMax , y, Screen.GenerateBrush(fg,bg,chr,cp), layer);

			}
			for (uint x = (uint)s.xMin; x <= (uint)s.xMax; x++) {

				int chr = styles [currentStyle, 4];

				Screen.SetPixel (x , (uint)s.yMin, Screen.GenerateBrush(fg,bg,chr,cp), layer);

				chr = styles [currentStyle, 7];


				Screen.SetPixel (x , (uint)s.yMax, Screen.GenerateBrush(fg,bg,chr,cp), layer);

			}

			// draw corner pieces

			Screen.SetPixel ((uint)s.xMin , (uint)s.yMin, Screen.GenerateBrush(fg,bg,styles [currentStyle, 0],cp), layer);
			Screen.SetPixel ((uint)s.xMax , (uint)s.yMin, Screen.GenerateBrush(fg,bg,styles [currentStyle, 1],cp), layer);
			Screen.SetPixel ((uint)s.xMin , (uint)s.yMax, Screen.GenerateBrush(fg,bg,styles [currentStyle, 2],cp), layer);
			Screen.SetPixel ((uint)s.xMax , (uint)s.yMax, Screen.GenerateBrush(fg,bg,styles [currentStyle, 3],cp), layer);



		}

	}

	public enum Mode
	{
		MOUSE,
		CURSOR,
		BOX,
		YO
	}

    public static Painter instance;
    public static PainterInputFrame inputFrame;

    public static Color32 paintColor = new Color(0, 60, '#');

    public static int fg;
    public static int bg;
    public static int chr;
    public static int cp;

	public static Mode mode;

	static PaintBox paintBox = new PaintBox();

	Cursor cursor = new Cursor();
	bool primaryDown = false;


    static int brushSize = 1;
    public bool dirty = false;

    static bool splatter = false;

	static bool drawHelp = false;

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

		mode = Mode.MOUSE;

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

	public static void ShowHelp() {
		drawHelp = true;
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
		switch (mode) {
		case Mode.MOUSE:
			{
				brushIndex = newIndex;
				break;
			}
		case Mode.BOX:
			{
				paintBox.currentStyle = newIndex;
				break;
			}

		}
    }

    public static void SetBrushPage(int newPage)
	{
		switch (mode) {
		case Mode.MOUSE:
			{
				brushPage = newPage;
				break;
			}
		case Mode.BOX:
			{
				break;
			}

		}
    }


	public static void SetMode ( Mode newMode) {
		mode = newMode;
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



		DrawHelp ();
		DrawMode ();


        if (dirty)
            Landscape.UpdateTextureFromBuffer();



		cursor.position = Screen.pointerPos;
		cursor.Update ();



		switch (mode) {

		case Mode.BOX:
			{
				paintBox.Draw ();
				break;

			}
		case Mode.MOUSE:
			{
				
				if (!Palette.instance.active) {

					Color32 brush = GetPaintBrush ();

					if (instance.cursor.active && !instance.primaryDown)
						brush =  Screen.GenerateBrush (60, 60, 219, 0);

					dirty = true;
					uint offset = (uint)(brushSize / 2);
					for (uint x = 0; x < brushSize; x++) {
						for (uint y = 0; y < brushSize; y++) {
							if (splatter) {
								if (Random.value > 0.9)
									Screen.SetPixel ((uint)cursor.position.x + x - offset, (uint)cursor.position.y + y - offset, brush, Screen.Layer.FLOATING);
							} else {
								Screen.SetPixel ((uint)cursor.position.x + x - offset, (uint)cursor.position.y + y - offset, brush, Screen.Layer.FLOATING);
							}
						}
					}
				}
				break;
			}

		}

		DrawBrushBar();
			





    }


	void DrawHelp() {
		if (drawHelp) {
			
			GUI.DrawString (2, 4, "Fn:     Select Char", Screen.GenerateBrush ());
			GUI.DrawString (2, 5, "Alt-Fn: Select Char Pg", Screen.GenerateBrush ());
			GUI.DrawString (2, 6, "Space:  Toggle Pallette", Screen.GenerateBrush ());
			GUI.DrawString (2, 7, "RMB:    Brush Picker", Screen.GenerateBrush ());
			GUI.DrawString (2, 8, "LMB:    Paint", Screen.GenerateBrush ());
			GUI.DrawString (2, 9, "R:      Toggle Scatter", Screen.GenerateBrush ());
			GUI.DrawString (2, 10, "S:      Save", Screen.GenerateBrush ());

			drawHelp = false;
		}

	}

	void DrawMode() {
		GUI.DrawString (0, 32, mode.ToString(), Screen.GenerateBrush ());

	}

    void DrawBrushBar()
    {
        GUI.DrawBox(0, 0, 66, 2, Screen.GenerateBrush());
        GUI.DrawString((int)0, 0, "Pg"+(brushPage+1).ToString(), Screen.GenerateBrush(35,1));

		switch (mode) {
		case Mode.MOUSE:
			{

				for (int i = 0; i < 10; i++)
				{

					uint x = (uint)(4 + (i * 4));

					Color32 brush = Screen.GenerateBrush(60);
					if (brushIndex == i)
					{
						brush = Screen.GenerateBrush(63);
					}
					int slot = (i + (brushPage * 10));

					GUI.DrawString((int)x, 0, "F"+((i + 1)%10).ToString(), brush);
					Screen.SetPixel(x + 2, 0, brushes[slot], Screen.Layer.FLOATING);

				}				
				break;
			}
		case Mode.BOX: 
			{
				Color32 brush;
				for (int i = 0; i < 7; i++)
				{
					int chr = paintBox.styles [i, 0];
					int cp = paintBox.styles [i, 8];

					uint x = (uint)(4 + (i * 4));

					int color = (brushIndex == i) ? 63 : 60;

					int slot = (i + (brushPage * 10));

					GUI.DrawString((int)x, 0, "F"+((i + 1)%10).ToString(), Screen.GenerateBrush(color, 0, chr, 0));

					//brush.g = (byte)fg;
					//brush.b = (byte)bg;
					Screen.SetPixel(x + 2, 0, Screen.GenerateBrush(color, 0, chr, cp), Screen.Layer.FLOATING);

				}					

				break;
			}
		}


		GUI.DrawString((int)48, 0, "Alt-H : Help", Screen.GenerateBrush());


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
    }

    public static void SetCurrentBrush(Color32 newBrush)
    {
		switch (mode) {
		case Mode.MOUSE:
			{
				int slot = (brushIndex + (brushPage * 10));

				brushes[slot] = newBrush;
				PlayerPrefs.SetString("Brush"+slot, ColorToHex(newBrush));
				PlayerPrefs.Save();				
				break;
			}
		case Mode.BOX:
			{
				paintBox.brush = newBrush;
				break;

			}
		}

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

	void StartBox() {
		paintBox.SetTopLeft (Screen.pointerPos);
		paintBox.SetBottomRight (Screen.pointerPos);

		paintBox.preview = true;
	}

	void CommitBox() {
		EndBox ();

	}

	void EndBox() {
		paintBox.preview = false;

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
		switch (mode) {

			case Mode.MOUSE:
				{
					Paint(pos);
					break;

				}
			case Mode.BOX:
				{
					StartBox ();
					break;

				}
		}
		primaryDown = true;
    }

    public override void PrimaryDrag(Vector2 pos)
    {
		switch (mode) {

			case Mode.MOUSE:
				{
					Paint (pos);
					break;
				}
			case Mode.BOX:
				{
					paintBox.SetBottomRight (pos);
					break;
				}
		}
	
    }

    public override void PrimaryUp()
    {
		switch (mode) {

		case Mode.MOUSE:
			{
				break;

			}
		case Mode.BOX:
			{
				CommitBox ();
				break;

			}
		}

		primaryDown = false;

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
