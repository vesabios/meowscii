using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public static PZone currentZone;
    static PZone lastZone;

    static World instance;

    public static List<PD> staticObjects;

    public static Rect view;

    static Vector2 playerViewMargin = new Vector2(16, 12);

    public Vector2 viewLocation;

    void Awake()
    {
        instance = this;

        view = new Rect(0, 0, Screen.dims.x, Screen.dims.y);
        staticObjects = new List<PD>();

    }


    public static void GenerateNewWorld()
    {
        // create a new world from scratch and save it to disk

        PZone zone = PZoneDatabase.GetZone("TestAlpha");
        if (zone == null) Debug.Log("ZONE NOT FOUND");
        GameData.AddPZone(zone);

        LoadZone(zone);

        LoadZoneData();
        

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

        foreach (PD pd in GameData.data)
        {
            if (pd is PWorldObject)
            {
                PWorldObject worldObject = (PWorldObject)pd;
                if (worldObject.zoneID == currentZone.guid)
                    ((PWorldObject)pd).Draw();
            }
        }
        // draw vfx

    }

    static void CalculateView()
    {

        if (Engine.player.location.x > view.x + Screen.dims.x-playerViewMargin.x)
        {
            view.x += 1;
        } else if (Engine.player.location.x < view.x + playerViewMargin.x)
        {
            view.x -= 1;
        }

        if (Engine.player.location.y > view.y + Screen.dims.y - playerViewMargin.y)
        {
            view.y += 1;
        }
        else if (Engine.player.location.y < view.y + playerViewMargin.y)
        {
            view.y -= 1;
        }

        view.x = Mathf.Clamp(view.x, 0, Landscape.dims.x - Screen.dims.x);
        view.y = Mathf.Clamp(view.y, 0, Landscape.dims.y - Screen.dims.y);



    }
}
