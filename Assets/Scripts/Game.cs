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


    public static int WORLD_DIM = 256;

    void Awake()
    {
        instance = this;
    }


    public static void ShowListBox()
    {

        List<PItem> allItems = ItemDatabase.GetAllItems();

        List<string> displayItems = new List<string>();
        foreach (PItem item in allItems)
        {
            displayItems.Add(item.shortDisplayName);
        }

        /*
        ListBox lb = instance.gameObject.AddComponent<ListBox>();
        lb.SetItems(displayItems);
        lb.Show(1, 1, 30, 10);
        */
    }

    public static void CreateNewGame()
    {
        GameData.CreateNewGame();

        PActor player = ActorDatabase.GetActor("Human");
        player.location = new Vector3(25, 25);

        GameData.AddPActor(player);
        player.zoneID = World.currentZone.guid;

        Engine.player = player;
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

    public override void ScreenUpdate()
    {
        World.Draw();

        if (PlayerInputFrame.IsActive())
        {
            cursorBlinkTimer += Time.deltaTime;
            cursorMoveTimer += Time.deltaTime;
            if (cursorMoveTimer > 0.05f)
                cursorMoveTimer = 0;

            if (cursorBlinkTimer < 0.35f)
                Screen.SetPixel(cursorLocation, Screen.GenerateBrush(63, 20, 'X'), Screen.Layer.FLOATING);
            if (cursorBlinkTimer > 0.35f)
                cursorBlinkTimer = 0;
        }

    }

    public static bool CanActorOccupyLocation(PActor actor, Vector2 location)
    {

        return Landscape.IsLocationTraversable(location);
        return true;
    }
 


}
