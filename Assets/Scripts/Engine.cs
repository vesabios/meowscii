using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Engine : MonoBehaviour {

    public enum Mode
    {
        NULL,
        INIT,
        GAMEPLAY,
        TURN,
        LANDSCAPE,
        PAINT,
    }

    Logitech gamepad;
    InputFrame nullInputFrame;
    InputFrame playerInputFrame;

    public static PActor player;
    public static Engine instance;

    public static Mode mode;

    float turnDelay = 0.1f;

    void Awake()
    {
        instance = this;
    }

	void Start () {

        mode = Mode.INIT;

        // general systems
        gameObject.AddComponent<Serialization>();
        gameObject.AddComponent<GameData>();
        gameObject.AddComponent<World>();


        // screen components
        // order is important! last item added is first item drawn

        gameObject.AddComponent<ObjectEditor>();
        gameObject.AddComponent<Palette>();
        gameObject.AddComponent<Painter>();
        gameObject.AddComponent<InkConsole>();
        gameObject.AddComponent<GUI>();
        gameObject.AddComponent<Game>();


        // input systems
        gamepad = gameObject.AddComponent<Logitech>();
        nullInputFrame = gameObject.AddComponent<NullInputFrame>();
        playerInputFrame = gameObject.AddComponent<PlayerInputFrame>();

        nullInputFrame.Activate();
        playerInputFrame.Activate();

		InkManager.StartStory ();


        Game.CreateNewGame();

        BeginNewGame();

    }

    public static void BeginNewGame()
    {

        mode = Mode.GAMEPLAY;

        //Game.ShowListBox();

    }


    void Update () {

        Screen.PrepareBuffer();

        
        InputDevice.PollDevices();
        InputFrame.PollKeyboard();
        InputFrame.PollMouse();
        Screen.PollMouse();

        ScreenComponent.DrawAllComponents();

        Screen.DrawBuffer();


    }

    public static bool IsReady()
    {
        if (mode == Mode.NULL) return false;
        if (mode == Mode.INIT) return false;
        if (mode == Mode.TURN) return false;
        return true;
    }

    public static void ProcessTurn()
    {
        instance.StartCoroutine(instance.IProcessTurn());
    }

    IEnumerator IProcessTurn()
    {
        SetMode(Mode.TURN);

        // update all actors and other game things


        yield return new WaitForSeconds(turnDelay);
        SetMode(Mode.GAMEPLAY);
    }

    static void SetMode(Mode newMode)
    {
        mode = newMode;
    }

}
