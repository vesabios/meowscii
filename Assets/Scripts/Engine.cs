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

	public static System.Random random;

	static float extraDelay;


    float turnDelay = 0.1f;

    void Awake()
    {
        instance = this;
		random = new System.Random();
    }

	void Start () {

        mode = Mode.INIT;

        // general systems
        gameObject.AddComponent<Serialization>();
        gameObject.AddComponent<GameData>();
        gameObject.AddComponent<World>();


        // screen components
        // order is important! last item added is first item drawn

		gameObject.AddComponent<StringInputDialog> ();
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

	public static void Delay(float t) {
		extraDelay += t;
	}

	public static void ProcessTurn(int actionPoints)
    {
        instance.StartCoroutine(instance.IProcessTurn(actionPoints));
    }

	IEnumerator IProcessTurn(int actionPoints)
    {
        SetMode(Mode.TURN);

		Dijkstra.PrepareBaseGraph ();

        // update all actors and other game things
		foreach (PD pd in GameData.data)
		{
			if (pd is PActor)
			{
				PActor actor = (PActor)pd;
				if (actor.zoneID == World.currentZone.guid) {
					actor.Tick (actionPoints);
					if (extraDelay > 0) {
						yield return new WaitForSeconds(extraDelay);
						extraDelay = 0;

					}

				}
			}
		}

        yield return new WaitForSeconds(turnDelay);



        SetMode(Mode.GAMEPLAY);
    }

    static void SetMode(Mode newMode)
    {
        mode = newMode;
    }


	public static int AttackRoll(int mean) {
		return NormalRoll(mean, 3.5f);
	}

	/*
        95% of values are within 
        2 standard deviations of the mean
        a stddev of 2.5 gives us a range of 
        5 for stddev and 10 for 2 stddev
    */

	public static int DamageRoll(int mean) {
		float std_dev = ((float)mean) * 0.1f;
		std_dev = Mathf.Max(0.5f, std_dev);
		return Mathf.Max(1, NormalRoll(mean, std_dev));
	}

	public static int NormalRoll(int mean, float stdDev = 3.5f) {
		if (random == null) random = new System.Random();
		float u1 = (float)random.NextDouble(); //these are uniform(0,1) random doubles
		float u2 = (float)random.NextDouble();
		float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
		float randNormal = (mean-0.5f) + stdDev * randStdNormal; //random normal(mean,stdDev^2)

		return Mathf.RoundToInt(randNormal);
	}


}
