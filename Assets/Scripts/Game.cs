using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game : ScreenComponent {

    static Game instance;

    public static Vector2 cursorLocation;
    static float cursorBlinkTimer = 0;
    static float cursorMoveTimer = 0;

    public enum Critical
    {
        x2,
        x3,
        x4,

    }

    public enum Currency
    {
        CP,
        SP,
        GP,
        PP
    }

    public enum Size
    {
        FINE,
        DIMINUTIVE,
        TINY,
        SMALL,
        MEDIUM,
        LARGE,
        HUGE,
        GARGANTUAN,
        COLOSSAL

    }

    public enum DamageType
    {
        BLUDGEONING,
        PIERCING,
        SLASHING
    }


    void Awake()
    {
        instance = this;

		ObjectLibrary.Init ();
		ActorLibrary.Init();
		ZoneLibrary.Init();

    }


    public static void ShowListBox()
    {

        List<PItem> allItems = ItemDatabase.GetAllItems();

        List<string> displayItems = new List<string>();
        foreach (PItem item in allItems)
        {
            displayItems.Add(item.shortDisplayName);
        }

    }

	public static PActor CreateActor(string name, Vector3 location) {

		PActor a = ActorDatabase.GetActor(name);
		Debug.Log ("getting actor: " + name);
		a.location = location;
		GameData.AddPActor(a);
		a.zoneID = World.currentZone.guid;

		return a;
	}

    public static void CreateNewGame()
    {
        GameData.CreateNewGame();

		PActor player = CreateActor("Human", new Vector3(25,25,0));


		for (int i = 0; i < 5; i++) {
			CreateActor("Kobold", new Vector3(Random.Range(30,40),Random.Range(30,50),0));

		}

        Engine.player = player;

		World.FocusOnObject (player);
    }

    public static void MoveCursor(Vector2 moveVector)
    {

        cursorBlinkTimer = 0;
        if (cursorMoveTimer > 0) return;

        cursorLocation.x += moveVector.x;
        cursorLocation.y -= moveVector.y;

        cursorLocation.x = Mathf.Clamp(cursorLocation.x, 0, Screen.dims.x);
        cursorLocation.y = Mathf.Clamp(cursorLocation.y, 0, Screen.dims.y);
    }

	public void OnUpdatePointer() {
		cursorLocation = Screen.pointerPos;
	}

    public override void ScreenUpdate()
    {
        World.Draw();

		Inspector.Draw ();

		VFX.VFXBase.Draw ();

		if (Inspector.pointerOverride) {
			cursorLocation = Inspector.pointerPos;
		} else {
			cursorLocation = Screen.pointerPos;

		}


        if (PlayerInputFrame.IsActive())
        {
			Screen.SetHardwareCursorPosition (cursorLocation);

        }

    }

    public static bool CanActorOccupyLocation(PActor actor, Vector2 location)
    {

        return Landscape.IsLocationTraversable(location);
        return true;
    }
 


}
