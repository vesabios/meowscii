using UnityEngine;
using System.Collections;

public class Painter : ScreenComponent {

	class Cursor {

		public Vector2 position;
		public void Update() {

			if (position.y < 2) {
				if (World.view.y > -2) {
					World.ScrollView (new Vector2 (0, -1));
				}
			}

			if (position.y > Screen.dims.y-1) {
				if (World.view.y < Landscape.dims.y- Screen.dims.y) { 
					World.ScrollView (new Vector2 (0, 1));
				}
			}

			position.y = Mathf.Max (2, position.y);
			position.y = Mathf.Min (Screen.dims.y-1, position.y);

			Screen.SetHardwareCursorPosition (position);
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

    public static Color32 currentBrush = new Color(0, 60, '#');
	static bool showExtendedBar = false;


    public static int fg;
    public static int bg;
    public static int chr;
    public static int cp;

	public static Mode mode;

	static bool showCursorPreview = false;

	static PaintBox paintBox = new PaintBox();

	static Cursor cursor = new Cursor();
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

        currentBrush = Screen.GenerateBrush(60, 0, '#');
        active = false;

        brushes = new Color32[100];

		mode = Mode.MOUSE;

        GetSavedBrushes();

		Palette.SetSelectionFromBrush (brushes [0]);

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
		World.SetViewOffset(new Vector2(0,2));
		cursor.position = new Vector3 (0, 2);
		World.SetViewMode (World.ViewMode.FREE);
    }

    public static void Deactivate()
    {
        Debug.Log("deactivating painter");
        instance.active = false;
        PlayerInputFrame.instance.Activate();
		World.SetViewOffset(new Vector2(0,0));
					
		World.SetViewMode (World.ViewMode.WORLD_OBJECT);


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

				int slot = (brushIndex + (brushPage * 10));

				currentBrush = brushes [slot];

				break;
			}
		case Mode.BOX:
			{
				paintBox.currentStyle = newIndex;
				break;
			}

		}
    }

	public static void IncrementBrushPage() {
		brushPage++;
		if (brushPage > 10)
			brushPage = 10;
	}

	public static void DecrementBrushPage() {
		brushPage--;
		if (brushPage < 0)
			brushPage = 0;
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
				paintBox.currentStyle = newPage;

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

					dirty = true;

					if (showCursorPreview) {
						

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

				}
				break;
			}

		}

		DrawBrushBar();

    }


	public static void MoveCursor(Vector2 moveVector) {
		showCursorPreview = false;
		cursor.position += moveVector;
		cursor.position.x = Mathf.Max (0, cursor.position.x);
		cursor.position.y = Mathf.Max (0, cursor.position.y);

	}


	void DrawHelp() {
		if (drawHelp) {
			
			GUI.DrawString (2, 4, "Fn      Select Char", Screen.GenerateBrush ());
			GUI.DrawString (2, 5, "PgUp/Dn Select Char Pg", Screen.GenerateBrush ());
			GUI.DrawString (2, 6, "Space   Toggle Palette", Screen.GenerateBrush ());
			GUI.DrawString (2, 7, "RMB     Brush Picker", Screen.GenerateBrush ());
			GUI.DrawString (2, 8, "Alt.RMB Brush Picker & Store", Screen.GenerateBrush ());
			GUI.DrawString (2, 9, "LMB     Paint", Screen.GenerateBrush ());
			GUI.DrawString (2, 10, "Alt.R       Toggle Scatter", Screen.GenerateBrush ());
			GUI.DrawString (2, 11, "Alt.S      Save", Screen.GenerateBrush ());

			drawHelp = false;
		}

	}

	void DrawMode() {
		GUI.DrawString (0, 32, mode.ToString(), Screen.GenerateBrush ());

	}


	public static void ShowBar() {
		showExtendedBar = true; 
	}

	public static void HideBar() {
		showExtendedBar = false;
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

					GUI.DrawString((int)x, 0, ((i + 1)%10).ToString()+"=", brush);
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

					int color = (paintBox.currentStyle == i) ? 63 : 60;

					int slot = (i + (brushPage * 10));

					GUI.DrawString((int)x, 0, "F"+((i + 1)%10).ToString(), Screen.GenerateBrush(color, 0, chr, 0));


					Screen.SetPixel(x + 2, 0, Screen.GenerateBrush(fg, bg, chr, cp), Screen.Layer.FLOATING);

				}					

				break;
			}
		}


		GUI.DrawString((int)48, 0, "Alt-H : Help", Screen.GenerateBrush());


    }

	public static void TextInput(char s) {
		
		SetPixel((uint)cursor.position.x, (uint)cursor.position.y, Screen.GenerateBrush(currentBrush.g, currentBrush.b, (int)s));

		cursor.position.x += 1;

	}

    public static void GetSavedBrushes()
    {
        for (int i=0; i<100; i++)
            if (PlayerPrefs.HasKey("Brush"+i))
                brushes[i] = HexToColor(PlayerPrefs.GetString("Brush" + i));
    }


    public static void SetCurrentBrush(Color32 newBrush)
    {
		
		switch (mode) {
			case Mode.MOUSE:
				{

					currentBrush = newBrush;

					if (Input.GetKey (KeyCode.LeftAlt)) {

						int slot = (brushIndex + (brushPage * 10));

						brushes [slot] = newBrush;
						PlayerPrefs.SetString ("Brush" + slot, ColorToHex (newBrush));
						PlayerPrefs.Save ();	

					}
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
		return currentBrush;
//        return brushes[brushIndex];

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
		paintBox.Commit ();

	}

	void EndBox() {
		paintBox.preview = false;

	}

    public static void SetPixel(uint x, uint y, Color32 brush)
    {
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
					cursor.position = pos;

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

	public override void PrimaryMove(Vector2 pos) {
		showCursorPreview = true;
		cursor.position = pos;
	}

    public override void PrimaryDrag(Vector2 pos)
    {
		switch (mode) {

			case Mode.MOUSE:
				{
					cursor.position = pos;

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
				SetMode (Mode.MOUSE);
				break;

			}
		}

		primaryDown = false;

    }

    public override void SecondaryDown(Vector2 pos)
    {
        Color32 sample = Landscape.GetPixel((uint)Screen.pointerPos.x + (uint)World.view.x, (uint)Screen.pointerPos.y + (uint)World.view.y);

		Palette.SetSelectionFromBrush (sample);

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
