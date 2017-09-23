using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public enum ViewMode {
		NULL,
		FREE,
		WORLD_OBJECT
	}

	public static ViewMode viewMode = ViewMode.WORLD_OBJECT;
	static PWorldObject focusObject;

    public static PZone currentZone;
    static PZone lastZone;

    static World instance;

    public static List<PD> staticObjects;

    public static Rect view;

	public static Vector2 viewOffset = Vector2.zero;

    static Vector2 playerViewMargin = new Vector2(16, 12);

    public Vector2 viewLocation;

    void Awake()
    {
        instance = this;

        view = new Rect(0, 0, Screen.dims.x, Screen.dims.y);
        staticObjects = new List<PD>();

    }

	public static void SetViewOffset(Vector2 v) {
		viewOffset = v;
	}


    public static void GenerateNewWorld()
    {
        // create a new world from scratch and save it to disk

        PZone zone = ZoneDatabase.GetZone("Home");
        if (zone == null) Debug.Log("ZONE NOT FOUND");
        GameData.AddPZone(zone);

        LoadZone(zone);

        

    }


	public static PWorldObject GetObjectAt (Vector2 location) {

		foreach (PD pd in staticObjects)
		{
			if (pd is PWorldObject)
			{
				PWorldObject worldObject = (PWorldObject)pd;
				if (worldObject.zoneID == currentZone.guid) {
					if (worldObject.location == location)
						return worldObject;
				}
					
			}
		}

		// draw objects

		foreach (PD pd in GameData.data)
		{
			if (pd is PWorldObject)
			{
				PWorldObject worldObject = (PWorldObject)pd;
				if (worldObject.zoneID == currentZone.guid) {

					if (worldObject.location == location)
						return worldObject;
				}
			}
		}

		return null;


	}


    public static void AddWorldObject(PWorldObject worldObject)
    {
        worldObject.zoneID = currentZone.guid;
        staticObjects.Add(worldObject);
    }


    public static void ClearZone()
    {
        staticObjects.Clear();
    }

    public static void LoadZone(PZone newZone)
    {

        // the zone is responsible for preparing the landscape atlas
        newZone.LoadAtlas();

        //localObjects = GameData.GetObjectsInZone(newZone.guid);
        lastZone = currentZone;
        currentZone = newZone;

		LoadZoneData();


    }


    public static void StoreZoneData()
    {
        //Debug.Log("World::StoreZoneData, "+localObjects.Count+" objects");
        // serialize zone data to disk as permanent record

        instance.StartCoroutine(Serialization.instance.ISaveData(staticObjects, Application.dataPath + "/Resources/Atlas/" + currentZone.name +".pd"));

    }

    public static void LoadZoneData()
    {
        instance.StartCoroutine(Serialization.instance.ILoadData( Application.dataPath + "/Resources/Atlas/" + currentZone.name + ".pd"));
        Debug.Log(staticObjects.Count + " pd loaded");
    }

    static public void Draw () {

        CalculateView();


        // draw view of atlas

        // draw items

        // draw actors

        foreach (PD pd in staticObjects)
        {
            if (pd is PWorldObject)
            {
                ((PWorldObject)pd).Draw();
            }
        }

		// draw objects

        foreach (PD pd in GameData.data)
        {
            if (pd is PWorldObject)
            {
                PWorldObject worldObject = (PWorldObject)pd;
                if (worldObject.zoneID == currentZone.guid)
					if (!worldObject.BlocksMovement()) 
                    	((PWorldObject)pd).Draw();
            }
        }



		foreach (PD pd in GameData.data)
		{
			if (pd is PWorldObject)
			{
				PWorldObject worldObject = (PWorldObject)pd;
				if (worldObject.zoneID == currentZone.guid)
					if (worldObject.BlocksMovement()) 
						((PWorldObject)pd).Draw();
			}
		}

        // draw vfx

    }


	public static void FocusOnObject(PWorldObject obj) {
		focusObject = obj;
		viewMode = ViewMode.WORLD_OBJECT;
	}

    static void CalculateView()
    {

		Rect oldView = view;

		switch (viewMode) {
		case ViewMode.WORLD_OBJECT:
			{
				CalculateViewForActor (focusObject);
				break;
			}
		}

		if (view == oldView)
			return;



    }

	public static PActor GetActorByGuid(System.Guid guid) {
		foreach (PWorldObject worldObject in zoneObjects) {
			if (worldObject.guid == guid)
				return (PActor)worldObject;
		}
		return null;
	}

	static void CalculateViewForActor(PWorldObject a) {
		if (a.location.x > view.x + Screen.dims.x-playerViewMargin.x)
		{
			view.x += 1;
		} else if (a.location.x < view.x + playerViewMargin.x)
		{
			view.x -= 1;
		}

		if (a.location.y > view.y + Screen.dims.y - playerViewMargin.y)
		{
			view.y += 1;
		}
		else if (a.location.y < view.y + playerViewMargin.y)
		{
			view.y -= 1;
		}

		ClampView ();


	}

	public static void SetViewMode (ViewMode vm) {
		viewMode = vm;
	}


	public static void ScrollView (Vector2 scrollVector) {
		view.position += scrollVector;

	}
	static void ClampView() {
		view.x = Mathf.Clamp(view.x, 0, Landscape.dims.x - Screen.dims.x);
		view.y = Mathf.Clamp(view.y, 0, Landscape.dims.y - Screen.dims.y);
	}

	public static List<PWorldObject> zoneObjects = new List<PWorldObject>();
	public static List<PWorldObject> visibleObjects = new List<PWorldObject>();
	public static List<PWorldObject> notableObjects = new List<PWorldObject>();

	public static List<PWorldObject> GetWorldObjectsInZone() {

		foreach (PD pd in GameData.data)
		{
			if (pd is PWorldObject)
			{
				PWorldObject worldObject = (PWorldObject)pd;

				if (worldObject.zoneID == currentZone.guid) {
					if (!zoneObjects.Contains (worldObject)) {
						zoneObjects.Add (worldObject);
					}
				} else {
					if (zoneObjects.Contains (worldObject)) {
						zoneObjects.Remove (worldObject);
					}
				}
			}
		}

		return zoneObjects;
	}


	public static List<PWorldObject> GetVisibleObjects() {

		GetWorldObjectsInZone ();

		foreach (PWorldObject worldObject in zoneObjects) {

			if (World.view.Contains ((Vector2)worldObject.location)) {
				if (!visibleObjects.Contains (worldObject)) {
					visibleObjects.Add (worldObject);
				}
			} else {
				if (visibleObjects.Contains (worldObject)) {
					visibleObjects.Remove (worldObject);
				}
			}
		}


		return visibleObjects;



	}


	public static List<PWorldObject> GetNotableObjects() {

		GetVisibleObjects ();

		foreach (PWorldObject worldObject in visibleObjects) {

			if (worldObject is PActor) {
				if (((PActor)worldObject).IsAlive ()) {
					if (!notableObjects.Contains (worldObject)) {
						notableObjects.Add (worldObject);
					}
				} else if (notableObjects.Contains (worldObject)) {
					notableObjects.Remove (worldObject);
				}
			} else {
				if (notableObjects.Contains (worldObject)) {
					notableObjects.Remove (worldObject);
				}
			}
		}


		return notableObjects;



	}


}
