using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Palette : ScreenComponent {

	public static PaletteInputFrame inputFrame;
    public static Palette instance;
    Vector2 pos;

    int brushSize;

    Color32 sample;

	Color32 selection;

	static int fg = 63;
	static int bg = 0;
	static int character = 219;
	static int codepage = 0;
    static int displayCodepage = 0;

	GUIFrame panel;

	List<GUISwatch> swatches;
	List<GUICharacterSwatch> characterSwatches;

    void Awake()
    {
        instance = this;

		inputFrame = gameObject.AddComponent<PaletteInputFrame>();

		swatches = new List<GUISwatch> ();
		characterSwatches = new List<GUICharacterSwatch> ();

		sample = Screen.GenerateBrush ();
		selection = Screen.GenerateBrush ();



		CreateWindow();


    }

	public void CreateWindow() {
		panel = gameObject.AddComponent<GUIFrame>();
		panel.rect = new Rect(0, 0, 60, 13);
		panel.useBorder = false;
		GUIElement.rootElement = inputFrame;

		for (int y = 0; y < 2; y++) {
			for (int x = 0; x < 32; x++) {
				GUISwatch s = (GUISwatch)panel.AddChild<GUISwatch> (); 
				s.color = y*32+x;
				s.rect = new Rect(x, y, 1, 1);
				s.swatchSelect += SwatchSelect;

				swatches.Add (s);
			}
		}


		for (int y = 0; y < 8; y++) {
			for (int x = 0; x < 32; x++) {
				GUICharacterSwatch s = (GUICharacterSwatch)panel.AddChild<GUICharacterSwatch> (); 
				s.character = y*32+x;
				s.rect = new Rect(x, y+3, 1, 1);
				s.characterSwatchSelect += CharacterSwatchSelect;

				characterSwatches.Add (s);
			}
		}

		//SwatchSelect (61, true);
		//SwatchSelect (0, false);

		/*
		GUIText headerText = (GUIText)panel.AddChild<GUIText>();
		headerText.text = "Object Selector";
		headerText.rect = new Rect(0, 0, 60, 1);
		*/


	}

	void CharacterSwatchSelect(int c, int cp) {
		for (int i = 0; i < characterSwatches.Count; i++) {
			characterSwatches [i].selected = (c == i);
		}
		character = c;
		codepage = cp;

		UpdateSelectedBrush ();
	}

	void SwatchSelect(int colorIndex, bool foreground) {

		if (foreground) {
			fg = colorIndex;
			for (int i = 0; i < swatches.Count; i++) {
				swatches [i].fg = (colorIndex == i);
			}	
		} else {
			bg = colorIndex;
			for (int i = 0; i < swatches.Count; i++) {
				swatches [i].bg = (colorIndex == i);
			}
		}

		UpdateSelectedBrush ();

	}

	public void DestroyWindow() {
		Destroy(panel);

	}


	public static void Activate()
	{
		instance.active = true;
		inputFrame.Activate();
	}

	public static void Deactivate()
	{
		Debug.Log ("deactivating...");
		instance.active = false;
		Painter.Activate();

	}

	public void OnDestroy() {
		DestroyWindow();
	}

    public static void IncrementCodePage()
    {
        displayCodepage++;
		if (displayCodepage > 3) displayCodepage = 3;

		instance.UpdateCodepageDisplay ();
    }

    public static void DecrementCodePage()
    {
        displayCodepage--;
        if (displayCodepage <= 0) displayCodepage = 0;

		instance.UpdateCodepageDisplay ();

	}

	void UpdateCodepageDisplay() {
		for (int i = 0; i < characterSwatches.Count; i++) {
			characterSwatches [i].codepage = displayCodepage;
		}
	}

	public static void SetSelectionFromBrush(Color32 brush) {
		fg = brush.fg();
		bg = brush.bg();
		character = brush.character();
		codepage = brush.codepage();
	}

	void UpdateSelectedBrush() {
		selection = Screen.GenerateBrush (fg, bg, character, codepage);
		Painter.SetCurrentBrush (selection);
	}

	public override void ScreenUpdate () {

        if (active)
        {

			panel.Draw(new Vector2(0, 2));

			GUI.DrawBox(34,3,2,2,Screen.GenerateBrush(0,bg,32,0));
			GUI.DrawBox(33,2,2,2,Screen.GenerateBrush(0,fg,32,0));

			string fgs = Mathf.Round(fg).ToString();
			string bgs = Mathf.Round(bg).ToString();

			{
				int x = 37;

				GUI.DrawString(x, 3, "FG:" + fgs);
				GUI.DrawString(x, 4, "BG:" + bgs);
				GUI.DrawString(x, 5, "CH:"+character.ToString());
				GUI.DrawString(x, 6, "CP:"+codepage.ToString());

			}

        } 
    }



    void Paint()
    {


		/*

        sample = Screen.GetPixel(Screen.pointerPos, 1);

        if (sample.a == 0)
        {
            sample = Screen.GetPixel(Screen.pointerPos, 0);
        }

		*/


    }

    void DrawPalette(int ox, int oy)
    {
		/*
        int index = 0;

		GUI.DrawBox((int)ox, (int)oy, 60, 31, Screen.GenerateBrush());

        {
            for (int y=0; y<2; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    int palIndex = (y * 32) + x;

                    Color32 brush = Screen.GenerateBrush(index, index, 0);

                    if (palIndex == Painter.fg)
                        brush = Screen.GenerateBrush(63, index, 'F', Painter.cp);
                    else if (palIndex == Painter.bg)
                        brush = Screen.GenerateBrush(63, index, 'B', Painter.cp);

                    int xx = ox + x + 17;
                    int yy = oy + y + 1;

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

		*/

    }

    public void Show()
    {
		Activate ();
        active = true;
    }

    public void Hide()
    {
		Deactivate ();
        active = false;
    }


	public override void PrimaryDown(Vector2 pos)
	{
		if (panel.PrimaryDown(pos)) return;
	}

	/*
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
    */

    public override void PrimaryDrag(Vector2 pos)
    {

    }

    public override void PrimaryUp()
    {

    }

    public override void SecondaryDown(Vector2 pos)
    {

		if (panel.SecondaryDown(pos)) return;


		/*
        if (pos.x >= Screen.dims.x - 18)
        {
            Painter.bg = sample.b;
            Painter.UpdateCurrentBrush();
        } else
        {
            Painter.instance.SecondaryDown(pos);
        }
        */

    }



}
